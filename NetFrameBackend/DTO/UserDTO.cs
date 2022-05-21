using System;
using System.Collections.Generic;

#nullable disable

namespace NetFrameBackend.DTO
{
    public partial class UserDTO
    {

        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string verifyCode { get; set; }
    }
}
