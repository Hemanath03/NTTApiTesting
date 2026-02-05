using NTTApiTesting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace NTTApiTesting.Services
{
    public class ApiTestService
    {
        private readonly HttpClient _httpClient;
        private readonly ResponseValidator _validator;
        private readonly VariableManager _variableManager;

        public ApiTestService(VariableManager variableManager)
        {
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
            _validator = new ResponseValidator();
            _variableManager = variableManager;
        }

        public async Task<TestResult> ExecuteTestAsync(ApiTestCase testCase)
        {
            var testResult = new TestResult
            {
                TestName = testCase.Name,
                ExecutedAt = DateTime.Now,
                Method = testCase.Method
            };

            var stopwatch = Stopwatch.StartNew();

            try
            {
                // Replace variables in URL
                var url = _variableManager.ReplaceVariables(testCase.Url);
                testResult.Endpoint = url;

                // Create request
                var request = new HttpRequestMessage(
                    new HttpMethod(testCase.Method),
                    url
                );

                // Add headers with variable replacement
                foreach (var header in testCase.Headers)
                {
                    try
                    {
                        if (header.Key.Equals("Content-Type", StringComparison.OrdinalIgnoreCase))
                            continue;

                        var headerValue = _variableManager.ReplaceVariables(header.Value);
                        request.Headers.TryAddWithoutValidation(header.Key, headerValue);
                    }
                    catch (Exception ex)
                    {
                        Log.Warning($"Failed to add header {header.Key}: {ex.Message}");
                    }
                }

                // Add body with variable replacement
                if (!string.IsNullOrEmpty(testCase.Body))
                {
                    var body = _variableManager.ReplaceVariables(testCase.Body);
                    testResult.RequestBody = body;

                    var contentType = testCase.Headers.ContainsKey("Content-Type")
                        ? testCase.Headers["Content-Type"]
                        : "application/json";

                    request.Content = new StringContent(body, Encoding.UTF8, contentType);
                }

                // Execute request
                var response = await _httpClient.SendAsync(request);

                stopwatch.Stop();

                testResult.StatusCode = (int)response.StatusCode;
                testResult.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                testResult.ResponseBody = await response.Content.ReadAsStringAsync();
                Log.Information("RESPONSE | {TestName} | {StatusCode} | {Body}",
                   testResult.TestName,
                   testResult.StatusCode,
                   testResult.ResponseBody);
 

                // Extract token/variables from response if configured
                if (testCase.ExtractToken != null && !string.IsNullOrEmpty(testCase.ExtractToken.JsonPath))
                {
                    _variableManager.ExtractFromResponse(
                        testResult.ResponseBody,
                        testCase.ExtractToken.JsonPath,
                        testCase.ExtractToken.VariableName
                    );
                }

                // Validate response
                var validationResult = _validator.ValidateResponse(testCase, testResult);
                testResult.StatusCodeValid = testResult.StatusCode == testCase.ExpectedStatusCode;
                testResult.ResponseBodyValid = validationResult.isValid;
                testResult.IsPassed = validationResult.isValid;

                if (!validationResult.isValid)
                {
                    testResult.ErrorMessage = string.Join("; ", validationResult.errors);
                    testResult.ValidationErrors = string.Join(" | ", validationResult.errors);
                }
            }
            catch (Exception ex)
            {
                stopwatch.Stop();
                testResult.ResponseTimeMs = stopwatch.ElapsedMilliseconds;
                testResult.IsPassed = false;
                testResult.ErrorMessage = ex.Message;
                testResult.ValidationErrors = ex.Message;
                Log.Error(ex, $"Error executing test: {testResult.TestName}");
            }

            return testResult;
        }
    }
}