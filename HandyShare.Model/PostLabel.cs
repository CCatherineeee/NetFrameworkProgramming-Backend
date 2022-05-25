using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Model
{
    public partial class PostLabel
    {
        public int? PostId { get; set; }
        public string Label { get; set; }
        public int PostLabelId { get; set; }

        public virtual Post Post { get; set; }
    }
}
