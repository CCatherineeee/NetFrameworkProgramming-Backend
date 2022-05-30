using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.DTO
{
    public class PostDTO
    {
        public string title { set; get; }
        public string content { set; get; }
        public string pic_url { set; get; }
        public int user_id { set; get; }
        public List<string> labelList { set; get; }
    }
}
