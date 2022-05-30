
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

    }
}
