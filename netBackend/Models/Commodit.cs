using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class Commodit
    {
        public int CommoditId { get; set; }
        public int? Price { get; set; }
        public bool? HasPostage { get; set; }
        public int? Postage { get; set; }
        public string PicUrl { get; set; }
        public string Description { get; set; }
        public int? SellerId { get; set; }

        public virtual User Seller { get; set; }
    }
}
