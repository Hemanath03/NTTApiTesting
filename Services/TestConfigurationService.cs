using NTTApiTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTTApiTesting.Services
{
    public class TestConfigurationService
    {

        public List<ApiTestCase> GetLoginTestCase()
        {
            Console.WriteLine(AppContext.BaseDirectory);

            var loginCases = new List<ApiTestCase>();

            var filePath = @"C:\Users\Rober\source\repos\NTTApiTesting\Resources\NTTCoreCollection_developer.json";

            Console.WriteLine("Loading Postman collection from: " + filePath);

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"File not found: {filePath}");

            var json = File.ReadAllText(filePath);


            var collection = JsonSerializer.Deserialize<PostmanCollection>(
            json,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var testCases = new List<ApiTestCase>();

            foreach (var folder in collection.item)
            {
                if (folder.item == null) continue;

                foreach (var api in folder.item)
                {
                    if (api.request == null) continue;

                    var headers = api.request.header?
                        .ToDictionary(h => h.key, h => h.value)
                        ?? new Dictionary<string, string>();

                    testCases.Add(new ApiTestCase
                    {
                        Name = api.name,
                        Method = api.request.method,
                        Url = api.request.url?.raw,
                        Headers = headers,
                        Body = api.request.body?.raw,
                        ExpectedStatusCode = 200
                    });
                }
            }

            return testCases;

            loginCases.Add(new ApiTestCase
            {
                Name = "Send OTP API",
                Method = "POST",
                Url = "{{endpoint}}SendOTP",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "DEFAULT" },
                    { "Module", "DEFAULT" },
                    { "Source", "RMS" },
                    { "DeviceId", "MyAPI" }
                },
                Body = @"{
                    ""uid"": ""90255961"",
                    ""pwd"": ""Navia@123"",
                    ""TwoFA"": 1
                }",
                ExpectedStatusCode = 200,
            });

            return loginCases;
        }

        public List<ApiTestCase> GetTestCases()
        {
            var testCases = new List<ApiTestCase>();


            testCases.Add(new ApiTestCase
            {
                Name = "Login API - Extract Token",
                Method = "POST",
                Url = "{{endpoint}}Login",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "DEFAULT" },
                    { "Module", "DEFAULT" },
                    { "Source", "RMS" },
                    { "DeviceId", "MyAPI" }
                },
                Body = @"{
    ""uid"": ""90255961"",
    ""pwd"": ""Navia@123"",
    ""otp"": ""{{otp}}"",
    ""TwoFA"": 1
}",
                ExpectedStatusCode = 200,
                ExpectedResponseContains = new List<string> { "susertoken" },
                ExpectedJsonFields = new Dictionary<string, string>
                {
                    { "Status", "OK" },
                    { "ResponceDataObject.susertoken", "" }
                },
                ExtractToken = new TokenExtraction
                {
                    JsonPath = "ResponceDataObject.susertoken",
                    VariableName = "{{token}}"
                }
            });

            testCases.Add(new ApiTestCase
            {
                Name = "Get Holdings - Use Token",
                Method = "POST",
                Url = "{{endpoint}}Holdings",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""uid"": ""47054457"",
                    ""actid"": ""47054457"",
                    ""ClientType"": """",
                    ""SearchText"": """"
                }",
                ExpectedStatusCode = 200
            });


            testCases.Add(new ApiTestCase
            {
                Name = "Get Position Book - Use Token",
                Method = "POST",
                Url = "{{endpoint}}PositionBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""uid"": ""47054457"",
                    ""actid"": ""47054457"",
                    ""ClientType"": """"
                }",
                ExpectedStatusCode = 200
            });


            testCases.Add(new ApiTestCase
            {
                Name = "Get Order Book - Use Token",
                Method = "POST",
                Url = "{{endpoint}}OrderBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""actid"": ""47054457"",
                    ""uid"": ""47054457"",
                    ""prd"": """",
                    ""status"": ""AMO""
                }",
                ExpectedStatusCode = 200
            });




            /*
 
            
            // Template: Login and extract token
            testCases.Add(new ApiTestCase
            {
                Name = "Your Login API",
                Method = "POST",
                Url = "{{endpoint}}/YourLoginEndpoint",
                Headers = new Dictionary<string, string>
                {
                    { "Content-Type", "application/json" }
                },
                Body = @"{
                    ""username"": ""your_username"",
                    ""password"": ""your_password""
                }",
                ExpectedStatusCode = 200,
                ExtractToken = new TokenExtraction
                {
                    JsonPath = "data.token",  // Change to your JSON path
                    VariableName = "{{token}}"
                }
            });
 
            // Template: Use extracted token
            testCases.Add(new ApiTestCase
            {
                Name = "API Using Token",
                Method = "GET",
                Url = "{{endpoint}}/protected-endpoint",
                Headers = new Dictionary<string, string>
                {
                    { "Authorization", "Bearer {{token}}" },  // Token will be replaced
                    { "Content-Type", "application/json" }
                },
                ExpectedStatusCode = 200
            });
            */

            return testCases;
        }
    }

}