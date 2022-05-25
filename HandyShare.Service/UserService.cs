using HandyShare.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class UserService
    {
        private static readonly netContext _context = new netContext();

        public static async Task<bool> IsEmailRegistered(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if(user != null)
            {
                return true;
            }
            return false;
        }

        public static async void AddUser(User user)
        {
             _context.Users.Add(user);
            await _context.SaveChangesAsync();

        }

        public static async Task<bool> IsUserExistByName(string name)
        {
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Name == name);
            if (user_ == null)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> IsUserExistByEmail(string email)
        {
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user_ == null)
            {
                return false;
            }
            return true;
        }

        public static async Task<int> CheckPasswordByName(string name,string password)
        {
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Name == name);
            if (user_ == null)
            {
                return -1;
            }
            if(user_.Password == password)
            {
                return user_.UserId;

            }
            return -1;
        }

        public static async Task<int> CheckPasswordByEmail(string email, string password)
        {
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user_ == null)
            {
                return -1;
            }
            if (user_.Password == password)
            {
                return user_.UserId;

            }
            return -1;
        }

        public static async Task<bool> ResetPassword(string email, string password)
        {
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user_ == null)
            {
                return false;
            }
            user_.Password = password;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
