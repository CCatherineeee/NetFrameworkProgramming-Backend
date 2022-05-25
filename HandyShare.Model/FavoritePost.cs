
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Model
{
    public partial class FavoritePost
    {
        public int FavoriteId { get; set; }
        public int PostId { get; set; }

        public virtual Favorite Favorite { get; set; }
        public virtual Post Post { get; set; }
    }
}
