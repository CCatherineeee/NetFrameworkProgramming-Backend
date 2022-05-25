using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Model
{
    public partial class Follow
    {
        public int FollowId { get; set; }
        public int? FollowUserId { get; set; }
        public int? UserId { get; set; }

        public virtual User User { get; set; }
    }
}
