using System;
using System.Collections.Generic;

#nullable disable

namespace NetFrameBackend.Models
{
    public partial class PostLabel
    {
        public int? PostId { get; set; }
        public string Label { get; set; }
        public int PostLabelId { get; set; }

        public virtual Post Post { get; set; }
    }
}
