
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HandyShare.DTO;
using HandyShare.EmailHandler;
using HandyShare.Model;
using HandyShare.Response;
using HandyShare.Service;
using HandyShareOssStorageCLI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFrameBackend.Utils;

namespace NetFrameBackend.Controllers
{
    [Route("api/Posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly string prefix = "https://handyshare-1308588633.cos.ap-shanghai.myqcloud.com/";

        [HttpPost("UploadHeaderPic")]
        public async Task<IResponse> UploadHeaderPic([FromForm(Name = "file")] List<IFormFile> files)
        {
            List<string> urlList = new List<string>();
            files.ForEach(async file =>
            {
                var url = "";
                var fileName = file.FileName;
                var stream = file.OpenReadStream();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                await PostService.UploadPic(fileName, bytes);
                url = prefix + fileName;
                urlList.Add(url);
            });

            return new IResponse(200, urlList, null);

        }


        [HttpPost("UploadPic")]
        public async Task<IOssResponse> UploadPic([FromForm(Name = "file")] List<IFormFile> files)
        {
            List<string> urlList = new List<string>();
            files.ForEach(async file =>
            {
                String str = OssStorage.GenerateName();
                var url = "";
                var fileName = file.FileName;
                var stream = file.OpenReadStream();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                await PostService.UploadPic(str+fileName, bytes);
                url = prefix + str + fileName;
                urlList.Add(url);
            });
            IOssResponse res = new IOssResponse();
            res.errno = 0;
            res.data = new IOssResponseData();
            res.data.url = urlList[0];

            return res;
        }



        [HttpPost("addPost")]
        public async Task<IResponse> AddPost(PostDTO postDTO)
        {
            return await PostService.AddPost(postDTO);
        }

        [HttpGet]
        public async Task<IResponse> GetPost(int id)
        {
            var post = await PostService.GetPost(id);
            if (post != null)
            {
                return new IResponse(200, post, "获取成功！");
            }
            return new IResponse(500,null, "帖子不存在！");
        }

        [HttpGet("getMyPost")]
        public async Task<IResponse> GetMyPost(int id)
        {
            List<Post> posts = await PostService.GetPostByUserId(id);
            if (posts != null)
            {
                return new IResponse(200, posts, "获取成功！");
            }
            return new IResponse(500, null, "获取失败！");
        }

        [HttpPost("view")]
        public async Task<IResponse> view(int id)
        {
            var flag = await PostService.AddView(id);
            return new IResponse(200, null, null);
        }

        [HttpGet("hotPost")]
        public async Task<IResponse> GetHotPost()
        {
            var res = await PostService.GetHotPost();
            return new IResponse(200, res, null);

        }

        [HttpGet("newPost")]
        public async Task<IResponse> GetNewPost()
        {
            var res = await PostService.GetNewPost();
            return new IResponse(200, res, null);

        }



    }
}
