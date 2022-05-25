using System;

namespace HandyShare.Response
{
    public partial class Response
    {
        public Response(int code, object data, string msg)
        {
            this.code = code;
            this.data = data;
            this.msg = msg;
        }

        public int code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

    }

}
