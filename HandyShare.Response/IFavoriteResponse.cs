using HandyShare.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Response
{
    public class IFavoriteResponse
    {
        public string name { set; get; }
        public int favoriteId { set; get; }

        public List<FavoritePost> posts { set; get; }
    }
}
