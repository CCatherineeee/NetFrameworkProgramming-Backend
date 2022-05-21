using System;
using System.Collections.Generic;

#nullable disable

namespace NetFrameBackend.Models
{
    public partial class Favorite
    {
        public Favorite()
        {
            FavoritePosts = new HashSet<FavoritePost>();
        }

        public int FavoriteId { get; set; }
        public string FavoriteName { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<FavoritePost> FavoritePosts { get; set; }
    }
}
