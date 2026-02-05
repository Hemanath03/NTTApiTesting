using Newtonsoft.Json.Linq;
using NTTApiTesting.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Services
{
    public class ResponseValidator
    {
        public (bool isValid, List<string> errors) ValidateResponse(ApiTestCase testCase, TestResult result)
        {
            var errors = new List<string>();
            bool isValid = true;

            // Validate Status Code
            if (result.StatusCode != testCase.ExpectedStatusCode)
            {
                errors.Add($"Status code mismatch: Expected {testCase.ExpectedStatusCode}, Got {result.StatusCode}");
                isValid = false;
            }

            // Validate Response Contains
            if (testCase.ExpectedResponseContains != null && testCase.ExpectedResponseContains.Any())
            {
                foreach (var expectedText in testCase.ExpectedResponseContains)
                {
                    if (string.IsNullOrEmpty(result.ResponseBody) ||
                        !result.ResponseBody.Contains(expectedText, StringComparison.OrdinalIgnoreCase))
                    {
                        errors.Add($"Response missing expected text: '{expectedText}'");
                        isValid = false;
                    }
                }
            }

            // Validate JSON Fields
            if (testCase.ExpectedJsonFields != null && testCase.ExpectedJsonFields.Any())
            {
                try
                {
                    var jsonResponse = JObject.Parse(result.ResponseBody);

                    foreach (var expectedField in testCase.ExpectedJsonFields)
                    {
                        var token = jsonResponse.SelectToken(expectedField.Key);

                        if (token == null)
                        {
                            errors.Add($"JSON field not found: '{expectedField.Key}'");
                            isValid = false;
                        }
                        else if (!string.IsNullOrEmpty(expectedField.Value))
                        {
                            var actualValue = token.ToString();
                            if (!actualValue.Equals(expectedField.Value, StringComparison.OrdinalIgnoreCase))
                            {
                                errors.Add($"Field '{expectedField.Key}' value mismatch: Expected '{expectedField.Value}', Got '{actualValue}'");
                                isValid = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    errors.Add($"Failed to parse JSON response: {ex.Message}");
                    isValid = false;
                    Log.Warning($"JSON parsing error for test {testCase.Name}: {ex.Message}");
                }
            }

            return (isValid, errors);
        }
    }
}
