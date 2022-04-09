using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class Good
    {
        public Good()
        {
            Carts = new HashSet<Cart>();
            Orders = new HashSet<Order>();
        }

        public int GoodId { get; set; }
        public string DescriptionUrl { get; set; }
        public string PicUrl { get; set; }
        public int? Price { get; set; }
        public int? Num { get; set; }
        public byte? InSale { get; set; }
        public int? Likes { get; set; }
        public int? Sales { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
