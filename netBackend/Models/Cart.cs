using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class Cart
    {
        public int CartId { get; set; }
        public int? GoodId { get; set; }
        public int? UserId { get; set; }
        public int? Num { get; set; }

        public virtual Good Good { get; set; }
        public virtual User User { get; set; }
    }
}
