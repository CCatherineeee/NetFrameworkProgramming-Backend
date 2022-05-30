using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Response
{
    public class IOssResponseData
    {
        public string url { set; get; }
        public string alt { set; get; }
        public string href { set; get; }

    }
    public class IOssResponse
    {
        public int errno { set; get; }
        public IOssResponseData data { set; get; }
        
        public string message { set; get; }

    }
}
