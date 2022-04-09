using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class User
    {
        public User()
        {
            Addresses = new HashSet<Address>();
            Carts = new HashSet<Cart>();
            Comments = new HashSet<Comment>();
            Favorites = new HashSet<Favorite>();
            Goods = new HashSet<Good>();
            Orders = new HashSet<Order>();
            Posts = new HashSet<Post>();
        }

        public int UserId { get; set; }
        public string FakeName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Good> Goods { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
