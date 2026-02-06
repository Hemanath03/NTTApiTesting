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

            testCases.Add(new ApiTestCase
            {
                Name = "BasketOrder",
                Method = "POST",
                Url = "{{endpoint}}BasketOrder",
                Headers = new Dictionary<string, string>
                    {
                        { "AuthToken", "{{token}}" },
                        { "Module", "OrderService" },
                        { "Source", "RMS" }
                    },
                                Body = @"
{
                          ""AllOrders"": [
                            {
                              ""uid"": ""89191333"",
                              ""actid"": ""89191333"",
                              ""exch"": ""MCX"",
                              ""trantype"": ""Sell"",
                              ""norenordno"": ""0"",
                              ""segment"": ""CUR"",
                              ""tsym"": ""CRUDEOIL14JAN20266550CE"",
                              ""qty"": 1,
                              ""prc"": ""0.00"",
                              ""trgprc"": ""0"",
                              ""dscqty"": 0,
                              ""prd"": ""CNC"",
                              ""prctyp"": ""MKT"",
                              ""mkt_protection"": """",
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
                            },
                            {
                              ""uid"": ""89191333"",
                              ""actid"": ""89191333"",
                              ""exch"": ""MCX"",
                              ""trantype"": ""Buy"",
                              ""norenordno"": ""0"",
                              ""segment"": ""CUR"",
                              ""tsym"": ""CRUDEOIL14JAN20266600CE"",
                              ""qty"": 1,
                              ""prc"": ""0.00"",
                              ""trgprc"": ""0"",
                              ""dscqty"": 0,
                              ""prd"": ""CNC"",
                              ""prctyp"": ""MKT"",
                              ""mkt_protection"": """",
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
                            }
                  ]
                }",
                                ExpectedStatusCode = 200
                            });

            testCases.Add(new ApiTestCase
            {
                Name = "ModifyOrder",
                Method = "POST",
                Url = "{{endpoint}}ModifyOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    ""exch"": ""NSE"",
                    ""ordno"": ""1100000000121694"",
                    ""prctyp"": ""LMT"",
                    ""prc"": ""158.9"",
                    ""qty"": 2,
                    ""tqty"":10,
                    ""tsym"": ""IOC-EQ"",
                    ""ret"": """",
                    ""mkt_protection"": ""0"",
                    ""trgprc"": ""0"",
                    ""dscqty"": 0,
                    ""ext_remarks"": """",
                    ""cl_ord_id"": ""170256153"",
                    ""uid"": ""47054457"",
                    ""actid"": ""47054457"",
                    ""bpprc"": ""0"",
                    ""blprc"": ""0"",
                    ""trailprc"": ""0"",
                    ""ordersource"": ""Web"",
                    ""orderactivity"": 2,
                    ""prd"": ""CNC""
                }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "MWList",
                Method = "POST",
                Url = "{{endpoint}}MWList",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                    
                }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "CancelOrder",
                Method = "POST",
                Url = "{{endpoint}}CancelOrder",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                ""actid"": ""47054457"",
                ""uid"": ""47054457@123"",
                ""ordno"": ""150255663"",
                ""cl_ord_id"": ""150255663""
                    
                }",
                ExpectedStatusCode = 200

            });
            testCases.Add(new ApiTestCase
            {
                Name = "OrderBook",
                Method = "POST",
                Url = "{{endpoint}}OrderBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                ""actid"":""47054457"",
                ""uid"":""47054457""
                    
                }",
                ExpectedStatusCode = 200

            });
            testCases.Add(new ApiTestCase
            {
                Name = "PositionBook",
                Method = "POST",
                Url = "{{endpoint}}PositionBook",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                ""uid"":""47054457"",
                ""actid"":""47054457"",
                ""ClientType"":""""
                    
                }",
                ExpectedStatusCode = 200

            });
            testCases.Add(new ApiTestCase
            {
                Name = "GetUsedMargin",
                Method = "POST",
                Url = "{{endpoint}}GetUsedMargin",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                 ""uid"": ""47054457"",
                ""actid"":  ""47054457""
                    
                }",
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
                Body = @"{
                 ""uid"": ""47054457"",
                 ""actid"": ""47054457"",
                 ""ClientType"": """",
                 ""SearchText"":""""
                    
                }",
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
                Body = @"{
                  ""uid"":""47054457"",
                    ""ClientType"":"""",
                 ""actid"":""47054457""
                    
                }",
                ExpectedStatusCode = 200

            });
            testCases.Add(new ApiTestCase
            {
                Name = "GetUserInfo",
                Method = "POST",
                Url = "{{endpoint}}GetUserInfo",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                   ""actid"": ""47054457""                    
                }",
                ExpectedStatusCode = 200

            });
            testCases.Add(new ApiTestCase
            {
                Name = "AlterTOTP",
                Method = "POST",
                Url = "{{endpoint}}AlterTOTP",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                   ""uid"":""47054457"",
                    ""IsTOTPDisable"":1                    
                }",
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
                Body = @"{
                    ""uid"":""47054457"",
                  ""actid"":""47054457""                   
                }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "GetLastAMOOrderStatus",
                Method = "POST",
                Url = "{{endpoint}}GetLastAMOOrderStatus",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                  ""actid"": ""47054457"",
                 ""uid"": ""47054457""
            }",
                ExpectedStatusCode = 200

            });

            testCases.Add(new ApiTestCase
            {
                Name = "ProductConversion",
                Method = "POST",
                Url = "{{endpoint}}ProductConversion",
                Headers = new Dictionary<string, string>
                {
                    { "AuthToken", "{{token}}" },
                    { "Module", "OrderService" },
                    { "Source", "RMS" }
                },
                Body = @"{
                  ""uid"": ""75885422"",
                    ""login_token"": ""UQUHixrgPBGHp8L6aYENVLNTZZ0vTiRjEpK7ecBIk1u0JP9FIyPRBaHLHD597MB6O5jcB4xxZmbD67tZ+BXcH8bjZK6xzLgHZ1dru8lfs7U="",
                    ""actid"": ""75885422"",
                    ""UserId"": ""75885422"",
                    ""usrID"": ""75885422"",
                    ""end_type"": ""ProductConversion"",
                    ""exch"": ""NFO"",
                    ""trantype"": ""Buy"",
                    ""segment"": ""Derivative"",
                    ""series"": ""EQ"",
                    ""tsym"": ""NIFTY2620325500CE"",
                    ""qty"": ""72"",
                    ""prc"": ""0"",
                    ""prd"": ""CNC"",
                    ""remarks"": ""Product conversion"",
                    ""prevprd"": ""MIS"",
                    ""postype"": ""Day"",
                    ""token"": ""49808"",
                    ""ordersource"": ""ios"",
                    ""ext_remarks"": ""External remarks"",
                    ""deviceid"": ""C273143B-4D07-4E20-9A3B-70AB7A2C61DA"",
                    ""device_type"": ""2"",
                    ""IP_ADDRESS"": ""106.210.165.8"",
                    ""device"": ""ios"",
                    ""BOW_API_VERSION"": ""1.0.4"",
                    ""pna"": ""com.navia.all"",
                    ""App_Version"": ""5.5"",
                    ""app_type"": """",
                    ""norenordno"": ""0"",
                    ""trgprc"": ""0"",
                    ""dscqty"": ""0"",
                    ""prctyp"": ""Limit"",
                    ""mkt_protection"": ""5%"",
                    ""ret"": ""Type X"",
                    ""bpprc"": ""0"",
                    ""blprc"": ""0"",
                    ""trailprc"": ""0"",
                    ""cl_ord_id"": ""123"",
                    ""amo"": ""Yes"",
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
                    ""fillshares2"": ""-11111111"",
                    ""rorgqty"": ""0"",
                    ""orgtrgprc"": ""0""
            }",
                ExpectedStatusCode = 200

            });








            return testCases;
        }
    }

}
