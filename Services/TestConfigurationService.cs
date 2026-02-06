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
                Body = File.ReadAllText($"TestData/Login.json"),
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
                Name = "CheckLogin",
                Method = "POST",
                Url = "{{endpoint}}CheckLogin",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "DEFAULT" },
                    { "Module", "DEFAULT" },
                    { "Source", "RMS" },
                    { "DeviceId", "MyAPI" }
                },
                Body = File.ReadAllText($"TestData/CheckLogin.json"),
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
                Name = "Get Client Bank Info ",
                Method = "POST",
                Url = "{{endpoint}}GetClientBankInfo",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/CheckLogin.json"),
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
                Body = File.ReadAllText($"TestData/AddMarketWatchName.json"),
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
                Body = File.ReadAllText($"TestData/DelMarketWatchName.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Add Market Watch Symbols",
                Method = "POST",
                Url = "{{endpoint}}AddMarketWatchSymbols",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/AddMarketWatchSymbols.json"),
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
                Body = File.ReadAllText($"TestData/PlaceOrder.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Basket Order",
                Method = "POST",
                Url = "{{endpoint}}BasketOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/BasketOrder.json"),
                ExpectedStatusCode = 200

            });
          
            testCases.Add(new ApiTestCase
            {
                Name = "Modify Order",
                Method = "POST",
                Url = "{{endpoint}}ModifyOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/ModifyOrder.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "MW List",
                Method = "POST",
                Url = "{{endpoint}}MWList",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/MWList.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "MW List",
                Method = "POST",
                Url = "{{endpoint}}MWList",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/MWList.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Cancel Order",
                Method = "POST",
                Url = "{{endpoint}}CancelOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/CancelOrder.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Order Book",
                Method = "POST",
                Url = "{{endpoint}}OrderBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/OrderBook.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Order Book",
                Method = "POST",
                Url = "{{endpoint}}OrderBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/OrderBook.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Position Book",
                Method = "POST",
                Url = "{{endpoint}}PositionBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/PositionBook.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Holdings",
                Method = "POST",
                Url = "{{endpoint}}Holdings",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/Holdings.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Holdings",
                Method = "POST",
                Url = "{{endpoint}}Holdings",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/Holdings.json"),
                ExpectedStatusCode = 200

            });
         
            testCases.Add(new ApiTestCase
            {
                Name = "Limits",
                Method = "POST",
                Url = "{{endpoint}}Limits",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/Limits.json"),
                ExpectedStatusCode = 200

            });
         
            testCases.Add(new ApiTestCase
            {
                Name = "TradeBook",
                Method = "POST",
                Url = "{{endpoint}}TradeBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/TradeBook.json"),
                ExpectedStatusCode = 200

            });
         
            testCases.Add(new ApiTestCase
            {
                Name = "Get User Info",
                Method = "POST",
                Url = "{{endpoint}}GetUserInfo",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetUserInfo.json"),
                ExpectedStatusCode = 200

            });
         
            testCases.Add(new ApiTestCase
            {
                Name = "Get Security Info",
                Method = "POST",
                Url = "{{endpoint}}GetSecurityInfo",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetSecurityInfo.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "GetLastOrderStatus",
                Method = "POST",
                Url = "{{endpoint}}GetLastOrderStatus",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetLastOrderStatus.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "Get Last Order Status",
                Method = "POST",
                Url = "{{endpoint}}GetLastOrderStatus",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetLastOrderStatus.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "GetLastAMOOrderStatus ",
                Method = "POST",
                Url = "{{endpoint}}GetLastAMOOrderStatus ",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetLastAMOOrderStatus.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "ProductConversion ",
                Method = "POST",
                Url = "{{endpoint}}ProductConversion ",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/ProductConversion.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "MarketWatch ",
                Method = "POST",
                Url = "{{endpoint}}MarketWatch",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/MarketWatch.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "GetOrderMargin",
                Method = "POST",
                Url = "{{endpoint}}GetOrderMargin",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetOrderMargin.json"),
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "GetBasketMargin",
                Method = "POST",
                Url = "{{endpoint}}GetBasketMargin",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/GetBasketMargin.json"),
                ExpectedStatusCode = 200

            });
     
            testCases.Add(new ApiTestCase
            {
                Name = "DeleteMultiMWScrips",
                Method = "POST",
                Url = "{{endpoint}}DeleteMultiMWScrips",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = File.ReadAllText($"TestData/DeleteMultiMWScrips.json"),
                ExpectedStatusCode = 200

            });




            ////

            return testCases;
        }
    }

}
