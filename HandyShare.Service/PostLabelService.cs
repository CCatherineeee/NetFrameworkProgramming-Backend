using HandyShare.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class PostLabelService
    {
        private static readonly netContext _context = new netContext();
        public static async Task<bool> AddPostLabelList(List<string> labelList,int postId)
        {
            foreach(string label in labelList)
            {
                PostLabel postLabel = new PostLabel();
                postLabel.PostId = postId;
                postLabel.Label = label;
                _context.PostLabels.Add(postLabel);
            }
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
    }
}
