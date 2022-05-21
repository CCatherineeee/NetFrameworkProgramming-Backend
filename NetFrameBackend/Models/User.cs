using System;
using System.Collections.Generic;

#nullable disable

namespace NetFrameBackend.Models
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            Follows = new HashSet<Follow>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string AvatarUrl { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Follow> Follows { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
