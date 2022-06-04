using System;

namespace HandyShare.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string verifyCode { get; set; }

        public string picUrl { get; set; }
        public string Description { get; set; }


    }
}
