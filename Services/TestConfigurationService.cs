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

        public List<ApiTestCase> GetLoginTestCase()
        {
            var loginCases = new List<ApiTestCase>();

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
                    ""uid"": ""47054457"",
                    ""pwd"": ""Uat@47054457"",
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
                  ""uid"": ""47054457"",
                 ""pwd"": ""Uat@47054457"",
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

            testCases.Add(new ApiTestCase
            {
                Name = "Get Client Bank Info ",
                Method = "POST",
                Url = "{{endpoint}}GetClientBankInfo",
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
            testCases.Add(new ApiTestCase
            {
                Name = "Add Market Watch Name",
                Method = "POST",
                Url = "{{endpoint}}AddMarketWatchName",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""MarketWatchId"": ""78314"",
                    ""IsDeleted"": 0,
                    ""MarketWatchName"":""Test""
                }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Delete Market Watch Name",
                Method = "POST",
                Url = "{{endpoint}}AddMarketWatchName",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""MarketWatchId"": ""78314"",
                    ""IsDeleted"": 1,
                    ""MarketWatchName"":""Test""
                }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Place Order",
                Method = "POST",
                Url = "{{endpoint}}PlaceOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""uid"": ""47054457"",
                     ""actid"": ""47054457"",
                     ""exch"": ""NSE"",
                     ""trantype"": ""Buy"",
                     ""norenordno"": ""0"",
                     ""segment"": ""EQ"",
                     ""tsym"": ""IOC-EQ"",
                     ""qty"": 100,
                      ""prc"": ""170.00"",
                      ""trgprc"": ""0"",
                     ""dscqty"": 0,
                      ""prd"": ""CNC"",
                      ""prctyp"": ""Limit"",
                      ""mkt_protection"": ""0"",
                      ""ret"": ""DAY"",
                      ""remarks"": """",
                      ""ordersource"": ""Web"",
                       ""bpprc"": ""0"",
                       ""blprc"": ""0"",
                       ""trailprc"": ""0"",
                       ""ext_remarks"": ""External remarks"",
                      ""cl_ord_id"": """",
                        ""tsym2"": """",
                        ""trantype2"": """",
                        ""qty2"": ""0"",
                        ""prc2"": ""0"",
                      ""tsym3"": ""0"",
                      ""trantype3"": """",
                        ""qty3"": ""0"",
                      ""prc3"": ""0"",
                      ""algo_id"": ""0"",
                      ""naic_code"": ""0"",
                      ""snonum"": ""0"",
                     ""filshares"": ""0"",
                      ""rorgqty"": ""0"",
                        ""orgtrgprc"": ""0""
                }",
                ExpectedStatusCode = 200

            });










            return testCases;
        }
    }

}
