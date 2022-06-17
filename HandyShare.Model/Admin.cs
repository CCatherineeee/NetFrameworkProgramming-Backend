using System;
using System.Collections.Generic;

#nullable disable

namespace HandyShare.Models
{
    public partial class Admin
    {
        public int AdminId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
    }
}
