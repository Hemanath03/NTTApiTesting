using NTTApiTesting.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Services
{
    public class TestConfigurationService
    {
        public List<ApiTestCase> GetTestCases()
        {
            var testCases = new List<ApiTestCase>();

            // STEP 1: Login API - Extract Token Automatically
            testCases.Add(new ApiTestCase
            {
                Name = "Login API - Extract Token",
                Method = "POST",
                Url = "{{endpoint}}/Login",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "DEFAULT" },
                    { "Module", "DEFAULT" },
                    { "Source", "RMS" },
                    { "DeviceId", "MyAPI" }
                },
                Body = @"{
                    ""uid"": ""84599711"",
                    ""pwd"": ""84599711@123"",
                    ""otp"": ""696247"",
                    ""TwoFA"": 1
                }",
                ExpectedStatusCode = 200,
                ExpectedResponseContains = new List<string> { "susertoken" },
                ExpectedJsonFields = new Dictionary<string, string>
                {
                    { "Status", "OK" },
                    { "ResponceDataObject.susertoken", "" }
                },
                // Extract token from response and store as {{token}}
                ExtractToken = new TokenExtraction
                {
                    JsonPath = "ResponceDataObject.susertoken",
                    VariableName = "{{token}}"
                }
            });

            // STEP 2: Use the extracted token in subsequent requests
            testCases.Add(new ApiTestCase
            {
                Name = "Get Holdings - Use Token",
                Method = "POST",
                Url = "{{endpoint}}/Holdings",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },  // Will be replaced with actual token
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""uid"": ""84599711"",
                    ""actid"": ""84599711"",
                    ""ClientType"": """",
                    ""SearchText"": """"
                }",
                ExpectedStatusCode = 200
            });

            // STEP 3: Another API using the same token
            testCases.Add(new ApiTestCase
            {
                Name = "Get Position Book - Use Token",
                Method = "POST",
                Url = "{{endpoint}}/PositionBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },  // Will be replaced with actual token
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""uid"": ""84599711"",
                    ""actid"": ""84599711"",
                    ""ClientType"": """"
                }",
                ExpectedStatusCode = 200
            });

            // STEP 4: Order Book API
            testCases.Add(new ApiTestCase
            {
                Name = "Get Order Book - Use Token",
                Method = "POST",
                Url = "{{endpoint}}/OrderBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },  // Automatically uses extracted token
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""actid"": ""84599711"",
                    ""uid"": ""84599711"",
                    ""prd"": """",
                    ""status"": ""AMO""
                }",
                ExpectedStatusCode = 200
            });

            // Example with public API (no token needed)
            testCases.Add(new ApiTestCase
            {
                Name = "Public API - Get User",
                Method = "GET",
                Url = "https://jsonplaceholder.typicode.com/users/1",
                ExpectedStatusCode = 200,
                ExpectedResponseContains = new List<string> { "email" }
            });

            /* 
            ===============================================
            ADD YOUR API TEST CASES HERE
            ===============================================
            
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
