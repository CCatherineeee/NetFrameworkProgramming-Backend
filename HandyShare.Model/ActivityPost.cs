using System;
using System.Collections.Generic;

#nullable disable

namespace HandyShare.Models
{
    public partial class ActivityPost
    {
        public int ActivityId { get; set; }
        public int PostId { get; set; }

        public virtual Activity Activity { get; set; }
        public virtual Post Post { get; set; }
    }
}
