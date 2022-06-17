
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HandyShare.DTO;
using HandyShare.EmailHandler;
using HandyShare.Response;
using HandyShare.Service;
using HandyShareOssStorageCLI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFrameBackend.Utils;

namespace NetFrameBackend.Controllers
{
    [Route("api/Comments")]
    [ApiController]
    public class CommentsController : ControllerBase
    {

        [HttpPost("add")]
        public async Task<IResponse> Add(CommentDTO commentDTO)
        {
            var comment = await CommentsService.Add(commentDTO);
            if (comment != null)
            {
                return new IResponse(200, comment, "发布成功！");
            }
            else
            {
                return new IResponse(500, null, "发送失败！");

            }
        }
        [HttpGet("getByPostId")]
        public async Task<IResponse> GetByPostId(int postId)
        {
            var List = await CommentsService.GetCommentsByPostId(postId);
            return new IResponse(200, List, "获得成功！");
        }


    }
}