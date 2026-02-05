using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public string ErrorMessage { get; set; }
        public long ResponseTimeMs { get; set; }
        public DateTime ExecutedAt { get; set; }
        public string RequestBody { get; set; }
        public string ResponseBody { get; set; }

        // Validation Details
        public bool StatusCodeValid { get; set; }
        public bool ResponseBodyValid { get; set; }
        public string ValidationErrors { get; set; }
    }
}
