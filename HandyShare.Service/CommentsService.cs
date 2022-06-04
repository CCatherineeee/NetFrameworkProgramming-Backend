using HandyShare.DTO;
using HandyShare.Model;
using HandyShare.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class CommentsService
    {
        [DllImport("HandyShareWin32Dll.dll")]
        public extern static int Add(int x,int y);
        public static async Task<List<Comment>> GetCommentsByPostId(int id)
        {
            netContext _context = new netContext();
            var post = await _context.Posts.FindAsync(id);
            _context.Entry(post).Collection("Comments").Load();
            List<Comment> list = new List<Comment>();

            foreach (Comment model in post.Comments)
            {
                model.Post = null;
                _context.Entry(model).Reference("User").Load();
                list.Add(model);
            }
            return list;
        }
        public static async Task<Comment> Add(CommentDTO commentDTO)
        {
            netContext _context = new netContext();
            Comment comment = new Comment();
            comment.UserId = commentDTO.userId;
            comment.PostId = commentDTO.postId;
            comment.Content = commentDTO.content;
            comment.CreateTime = DateTime.Now;
            _context.Comments.Add(comment);
            var post = await _context.Posts.FindAsync(commentDTO.postId);
            post.CommrntCount = Add((int)post.CommrntCount, 1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            return comment;

        }

    }
}
