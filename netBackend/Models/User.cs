using System;
using System.Collections.Generic;

#nullable disable

namespace netBackend.Models
{
    public partial class User
    {
        public User()
        {
            Commodits = new HashSet<Commodit>();
        }

        public int UserId { get; set; }
        public string FakeName { get; set; }
        public bool? Identified { get; set; }
        public string UserPwd { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Commodit> Commodits { get; set; }
    }
}
