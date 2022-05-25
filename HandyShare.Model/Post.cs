using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Model
{
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            FavoritePosts = new HashSet<FavoritePost>();
            PostLabels = new HashSet<PostLabel>();
        }

        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PicUrl { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? FavoriteCount { get; set; }
        public int? CommrntCount { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavoritePost> FavoritePosts { get; set; }
        public virtual ICollection<PostLabel> PostLabels { get; set; }
    }
}
