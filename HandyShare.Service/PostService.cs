using HandyShare.DTO;
using HandyShare.Model;
using HandyShare.OssHandler;
using HandyShare.Response;
using HandyShareOssStorageCLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    public class PostService
    {
        private static readonly netContext _context = new netContext();
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
            Post post = new Post();
            post.UserId = postDTO.user_id;
            post.PicUrl = postDTO.pic_url;
            post.Title = postDTO.title;
            post.Content = postDTO.content;

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
    }
}
