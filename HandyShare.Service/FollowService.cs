using HandyShare.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class FollowService
    {
        public static async Task<bool>  Disfollow(int uid,int toid)
        {
            netContext _context = new netContext();
            var follow = await _context.Follows.FirstOrDefaultAsync(e=>e.UserId== uid && e.FollowUserId == toid);
            if (follow == null)
            {
                return false;
            }

            _context.Follows.Remove(follow);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static async Task<bool> Follow(int uid, int toid)
        {
            netContext _context = new netContext();
            
            var follow_ = await _context.Follows.FirstOrDefaultAsync(e => e.UserId == uid && e.FollowUserId == toid);
            if (follow_ == null)
            {
                Follow follow = new Follow();
                follow.UserId = uid;
                follow.FollowUserId = toid;
                _context.Follows.Add(follow);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    return false;
                }
                return true;

            }
            return false;
        }

        public static async Task<bool> IsFollow(int uid, int toid)
        {
            netContext _context = new netContext();

            var follow_ = await _context.Follows.FirstOrDefaultAsync(e => e.UserId == uid && e.FollowUserId == toid);
            if (follow_ != null)
            {
                return true;

            }
            return false;
        }
    }
}
