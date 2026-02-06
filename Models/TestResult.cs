using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NTTApiTesting.Models
{
    public class TestResult
    {
        public string TestName { get; set; }
        public string Endpoint { get; set; }
        public string Method { get; set; }
        public int StatusCode { get; set; }
        public bool IsPassed { get; set; }
        public bool IsOTPPassed { get; set; } = false;
        public string ErrorMessage { get; set; }
        public long ResponseTimeMs { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }
        public CommonApiResponse TypedResponse { get; set; }

        // Validation Details
        public bool StatusCodeValid { get; set; }
        public bool ResponseBodyValid { get; set; }
        public string ValidationErrors { get; set; }
    }
    public class CommonApiResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }
        public string RequestID { get; set; }
        public object Responce { get; set; }
        public ResponceDataObject ResponceDataObject { get; set; }


        public string Activity { get; set; }
        public string TypeID { get; set; }
        public string Info { get; set; }

    }

    public class ResponceDataObject
    {
        public object? Data { get; set; }
        public string request_time { get; set; }
        public string status { get; set; }
        public string Message { get; set; }
        public int? Result { get; set; }

    }
}
