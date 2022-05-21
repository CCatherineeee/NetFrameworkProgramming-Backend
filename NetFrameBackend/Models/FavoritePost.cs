using System;
using System.Collections.Generic;

#nullable disable

namespace NetFrameBackend.Models
{
    public partial class FavoritePost
    {
        public int FavoriteId { get; set; }
        public int PostId { get; set; }

        public virtual Favorite Favorite { get; set; }
        public virtual Post Post { get; set; }
    }
}
