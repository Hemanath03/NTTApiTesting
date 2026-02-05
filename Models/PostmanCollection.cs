using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTTApiTesting.Models
{
    public class PostmanCollection
    {
        public List<PostmanItem> item { get; set; }
    }

    public class PostmanItem
    {
        public string name { get; set; }
        public PostmanRequest request { get; set; }
        public List<PostmanItem> item { get; set; } // nested folders
    }

    public class PostmanRequest
    {
        public string method { get; set; }
        public List<PostmanHeader> header { get; set; }
        public PostmanBody body { get; set; }
        public PostmanUrl url { get; set; }
    }

    public class PostmanHeader
    {
        public string key { get; set; }
        public string value { get; set; }
    }

    public class PostmanBody
    {
        public string raw { get; set; }
    }

    public class PostmanUrl
    {
        public string raw { get; set; }
    }

}
