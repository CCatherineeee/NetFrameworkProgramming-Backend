using System;

namespace HandyShare.Response
{
    public partial class IResponse
    {
        public IResponse(int code, object data, string msg)
        {
            this.code = code;
            this.data = data;
            this.msg = msg;
        }
        public IResponse()
        {
        }

        public int code { get; set; }
        public object data { get; set; }
        public string msg { get; set; }

    }

}
