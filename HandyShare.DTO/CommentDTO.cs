using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.DTO
{
    public class CommentDTO
    {
        public int userId { get; set; }
        public int postId { get; set; }

        public string content { get; set; }
    }
}
