using System;
using System.Collections.Generic;

#nullable disable

namespace HandyShare.Models
{
    public partial class Activity
    {
        public Activity()
        {
            ActivityPosts = new HashSet<ActivityPost>();
        }

        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? EndTime { get; set; }

        public virtual ICollection<ActivityPost> ActivityPosts { get; set; }
    }
}
