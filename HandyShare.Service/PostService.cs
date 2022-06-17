using HandyShare.DTO;
using HandyShare.Models;
using HandyShare.OssHandler;
using HandyShare.Response;
using HandyShareOssStorageCLI;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class PostService
    {
        [DllImport("HandyShareWin32Dll.dll")]
        public extern static int Add(int x, int y);

        public static async Task<bool> calculateHotQuestionScoreValue(int id)
        {
            netContext _context = new netContext();

            var post = await _context.Posts.FindAsync(id);

            double factor1 = Math.Log10((double)post.ViewCount) * 4;

            double factor2 = (double)(post.FavoriteCount * post.CommrntCount / 5);

            TimeSpan ts = (TimeSpan)(DateTime.Now - post.CreateTime);

            double factor3 = ts.TotalSeconds;

            var comment = await _context.Comments.OrderByDescending(e => e.CreateTime).Where(e => e.PostId == id).FirstOrDefaultAsync();
            double factor4 = 1;
            if (comment != null)
            {
                TimeSpan ts2 = (TimeSpan)(DateTime.Now - comment.CreateTime);

                factor4 = Math.Pow((factor3 + 1) - ((factor3 - ts2.TotalSeconds) / 2), 1.5);
            }
            

            double res = (factor1 + factor2) / factor4;

            post.HotPoint = res;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return true;

        }


        public static async Task<IResponse> UploadPic(string FileName,byte[] bytes)
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

        public static async Task<IResponse> AddPost(PostDTO postDTO)
        {
            netContext _context = new netContext();
            Post post = new Post();
            post.UserId = postDTO.user_id;
            post.PicUrl = postDTO.pic_url;
            post.Title = postDTO.title;
            post.Content = postDTO.content;
            post.CreateTime = DateTime.Now;
            post.CommrntCount = 0;
            post.FavoriteCount = 0;
            _context.Posts.Add(post);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                return new IResponse(500, -1, "发布失败！");
            }
            var flag = await PostLabelService.AddPostLabelList(postDTO.labelList, post.PostId);
            if (flag)
            {
                return new IResponse(200, post.PostId, "发布成功！");
            }
            return new IResponse(500, -1, "发布失败！");
        }

        public static async Task<Post> GetPost(int id)
        {
            netContext _context = new netContext();
            var post = await _context.Posts.FindAsync(id);
            _context.Entry(post).Reference("User").Load();
            _context.Entry(post.User).Collection("Posts").Load();

            _context.Entry(post).Collection("Comments").Load();
            _context.Entry(post).Collection("PostLabels").Load();

            return post;
        }

        public static async Task<List<Post>> GetPostByUserId(int id)
        {
            netContext _context = new netContext();
            List<Post> posts = await _context.Posts.Include(e=>e.PostLabels)
                .Where(e => e.UserId == id).ToListAsync();

            return posts;
        }
        public static async Task<bool> AddView(int id)
        {
            netContext _context = new netContext();

            var post = await _context.Posts.FindAsync(id);
            post.ViewCount = Add((int)post.ViewCount, 1);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            await calculateHotQuestionScoreValue(id);
            return true;
        }

        public static async Task<List<Post>> GetHotPost()
        {
            netContext _context = new netContext();
            var posts = await _context.Posts.Include(e=>e.User).Include(e => e.PostLabels).OrderByDescending(e => e.HotPoint).Take(5).ToListAsync();
            return posts;
        }

        public static async Task<List<Post>> GetNewPost()
        {
            netContext _context = new netContext();
            var posts = await _context.Posts.Include(e => e.User).Include(e => e.PostLabels).OrderByDescending(e => e.CreateTime).Take(5).ToListAsync();
            return posts;
        }

    }
}
