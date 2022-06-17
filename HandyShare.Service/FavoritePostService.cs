using HandyShare.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class FavoritePostService
    {
        [DllImport("HandyShareWin32Dll.dll")]

        public extern static int Subtract(int x, int y);

        [DllImport("HandyShareWin32Dll.dll")]

        public extern static int Add(int x, int y);

        public static async Task<bool> AddFavoritePost(int id, int postId)
        {
            netContext _context = new netContext();
            FavoritePost favoritePost = new FavoritePost();
            favoritePost.PostId = postId;
            favoritePost.FavoriteId = id;
            _context.FavoritePosts.Add(favoritePost);

            var post = await _context.Posts.FindAsync(postId);
            if(post == null)
            {
                return false;
            }
            post.FavoriteCount = Add((int)post.FavoriteCount, 1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            await PostService.calculateHotQuestionScoreValue(postId);

            return true;
        }

        public static async Task<bool> DeleteFavoritePost(int id, int postId)
        {
            netContext _context = new netContext();
            var f = await _context.FavoritePosts.FirstOrDefaultAsync(e => e.FavoriteId == id && e.PostId == postId);

            if (f == null)
            {
                return false;
            }
            _context.FavoritePosts.Remove(f);
            var post = await _context.Posts.FindAsync(postId);
            if (post == null)
            {
                return false;
            }
            post.FavoriteCount = Subtract((int)post.FavoriteCount, 1);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            await PostService.calculateHotQuestionScoreValue(postId);

            return true;
        }

        public static async Task<int> Check(int id, int postId)
        {
            netContext _context = new netContext();
            List<Favorite> favorites = await _context.Favorites.Where(e => e.UserId == id).ToListAsync();
            foreach(Favorite favorite in favorites)
            {
                var f = await _context.FavoritePosts.FirstOrDefaultAsync(e => e.FavoriteId == favorite.FavoriteId && e.PostId == postId);
                if (f != null)
                {
                    return f.FavoriteId;
                }
            }


            return -1;
        }
    }
}
