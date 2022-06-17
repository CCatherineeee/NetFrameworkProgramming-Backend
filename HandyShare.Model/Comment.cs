using System;
using System.Collections.Generic;

#nullable disable

namespace HandyShare.Models
{
    public partial class Comment
    {
        public int? PostId { get; set; }
        public int? UserId { get; set; }
        public string Content { get; set; }
        public DateTime? CreateTime { get; set; }
        public int CommentId { get; set; }

        public virtual Post Post { get; set; }
        public virtual User User { get; set; }
    }
}
