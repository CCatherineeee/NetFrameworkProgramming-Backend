using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class Order
    {
        public int OrderId { get; set; }
        public int? UserId { get; set; }
        public int? GoodId { get; set; }
        public string Num { get; set; }
        public DateTime? CreateTime { get; set; }
        public int? Price { get; set; }
        public int? Status { get; set; }
        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }
        public virtual Good Good { get; set; }
        public virtual User User { get; set; }
    }
}
