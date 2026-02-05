using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Models
{
    public class ApiTestCase
    {
        public string Name { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; }
        public string Body { get; set; }
        public int ExpectedStatusCode { get; set; } = 200;

        
        public List<string> ExpectedResponseContains { get; set; }
        public Dictionary<string, string> ExpectedJsonFields { get; set; }

        
        public TokenExtraction ExtractToken { get; set; } 

        public ApiTestCase()
        {
            Headers = new Dictionary<string, string>();
            ExpectedResponseContains = new List<string>();
            ExpectedJsonFields = new Dictionary<string, string>();
        }
    }
}
