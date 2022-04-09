using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class Post
    {
        public Post()
        {
            CommentsNavigation = new HashSet<Comment>();
            FavoritePosts = new HashSet<FavoritePost>();
        }

        public int PostId { get; set; }
        public int? UserId { get; set; }
        public string HtmlUrl { get; set; }
        public int? Likes { get; set; }
        public int? Comments { get; set; }
        public string Title { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> CommentsNavigation { get; set; }
        public virtual ICollection<FavoritePost> FavoritePosts { get; set; }
    }
}
