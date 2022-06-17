using HandyShare.DTO;
using HandyShare.Models;
using HandyShare.OssHandler;
using HandyShare.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class UserService
    {

        public static async Task<bool> IsEmailRegistered(string email)
        {
            netContext _context = new netContext();
            var user = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if(user != null)
            {
                return true;
            }
            return false;
        }

        public static async void AddUser(User user)
        {
            netContext _context = new netContext();
            user.AvatarUrl = "https://handyshare-1308588633.cos.ap-shanghai.myqcloud.com/202264144139user1.jpg";
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            FavoriteDTO favoriteDTO = new FavoriteDTO();
            favoriteDTO.name = "默认收藏夹";
            favoriteDTO.userId = user.UserId;
            await FavoriteService.Create(favoriteDTO);

        }

        public static async Task<User> GetById(int id)
        {
            netContext _context = new netContext();
            var user  = await _context.Users.FindAsync(id);
            return user;

        }

        public static async Task<bool> IsUserExistByName(string name)
        {
            netContext _context = new netContext();
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Name == name);
            if (user_ == null)
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> IsUserExistByEmail(string email)
        {
            netContext _context = new netContext();
            var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Email == email);
            if (user_ == null)
            {
                return false;
            }
            return true;
        }

        public static async Task<int> CheckPasswordByName(string name,string password)
        {
            netContext _context = new netContext();
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
            netContext _context = new netContext();
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
            netContext _context = new netContext();
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

        public static async Task<IResponse> UploadPic(string FileName, byte[] bytes)
        {

            var cosClient = new CosBuilder()
                .SetAccount("1308588633", "ap-shanghai")
                .SetCosXmlServer()
                .SetSecret("AKIDrWv4xYtymJZZImzvue7JFtGMCm46sNbd", "qQknZ5KIzQaT5QVJq55oGRLyKPDk0zg7")
                .Builder();
            IBucketClient client = new BucketClient(cosClient, "handyshare", "1308588633");
            // ICosClient client = new CosClient(cosClient, "1308588633");
            // 建立一个存储桶
            /*            var result = await client.CreateBucket("fsdgerer");
                        Console.WriteLine("处理结果：" + result.msg);*/
            // 查询存储桶列表
            var c = await client.UpFile(FileName, bytes);
            // Console.WriteLine(c.msg + c.data);
            return c;
        }

        public static async Task<bool> ResetUser(UserDTO userDTO)
        {
            netContext _context = new netContext();
            var user = await _context.Users.FirstOrDefaultAsync(a => a.UserId == userDTO.Id);
            if (user == null)
            {
                return false;
            }
            user.AvatarUrl = userDTO.picUrl;
            user.Name = userDTO.Name;
            user.Description = userDTO.Description;
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

        public static async Task<List<User>> GetFollow(int id)
        {
            netContext _context = new netContext();
            var user = await _context.Users.FirstOrDefaultAsync(a => a.UserId == id);
            if (user == null)
            {
                return null;
            }
            _context.Entry(user).Collection("Follows").Load();
            List<User> userList = new List<User>();
            foreach(Follow follow in user.Follows)
            {
                User u = await _context.Users.FirstOrDefaultAsync(a => a.UserId == follow.FollowUserId);
                userList.Add(u);
            }
            return userList;

        }



    }
}
