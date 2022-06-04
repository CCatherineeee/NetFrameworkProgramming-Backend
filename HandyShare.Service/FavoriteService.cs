using HandyShare.DTO;
using HandyShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using HandyShare.Response;

namespace HandyShare.Service
{
    public class FavoriteService
    {
        public static async Task<bool> Create(FavoriteDTO favoriteDTO)
        {
            netContext _context = new netContext();
            Favorite favorite = new Favorite();
            favorite.UserId = favoriteDTO.userId;
            favorite.FavoriteName = favoriteDTO.name;
            var f = await _context.Favorites.FirstOrDefaultAsync(e => e.FavoriteName == favoriteDTO.name);
            if(f != null)
            {
                return false;
            }
             _context.Favorites.Add(favorite);

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

        public static async Task<List<IFavoriteResponse>> My(int id)
        {
            netContext _context = new netContext();
            List<Favorite> favorites = await _context.Favorites.Where(e => e.UserId == id).ToListAsync();
            List<IFavoriteResponse> list = new List<IFavoriteResponse>();
            foreach(Favorite favorite in favorites)
            {
                IFavoriteResponse ifr = new IFavoriteResponse();
                ifr.favoriteId = favorite.FavoriteId;
                ifr.name = favorite.FavoriteName;
                List<FavoritePost> favoritePosts = await _context.FavoritePosts.Include(e=>e.Post).Include(e=>e.Post.PostLabels).Where(e => e.FavoriteId == favorite.FavoriteId).ToListAsync();
                ifr.posts = favoritePosts;
                list.Add(ifr);
            }
            return list;
        }

    }
}
