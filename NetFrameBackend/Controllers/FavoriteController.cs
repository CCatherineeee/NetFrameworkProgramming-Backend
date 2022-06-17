using HandyShare.DTO;
using HandyShare.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    [Route("api/Favorite")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        [HttpPost("create")]
        public async Task<IActionResult> Create(FavoriteDTO favoriteDTO)
        {
            var flag = await FavoriteService.Create(favoriteDTO);
            if (flag)
            {
                return Ok(new IResponse(200, null, "创建成功！"));
            }
            return Ok(new IResponse(500, null, "名字重复！"));

        }

        [HttpGet("my")]
        public async Task<IActionResult> My(int id)
        {
            List<IFavoriteResponse> list = await FavoriteService.My(id);
            return Ok(new IResponse(200, list, "获取成功！"));

        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(int postId, int id)
        {
            var flag = await FavoritePostService.AddFavoritePost(id, postId);

            if(flag == true)
            {
                return Ok(new IResponse(200, null, "添加成功！"));

            }
            return Ok(new IResponse(500, null, "添加失败！"));

        }

        [HttpPost("check")]
        public async Task<IActionResult> Check(int postId, int id)
        {
            var flag = await FavoritePostService.Check(id, postId);
            if(flag != -1)
                return Ok(new IResponse(200, true,flag.ToString()));
            else
            {
                return Ok(new IResponse(200, false, null));

            }

        }

        [HttpPost("disfavorite")]
        public async Task<IActionResult> Disfavorite(int postId, int id)
        {
            var flag = await FavoritePostService.DeleteFavoritePost(id, postId);
            if (flag == true)
                return Ok(new IResponse(200, null, "取消成功！"));
            else
            {
                return Ok(new IResponse(500, false, "取消失败！"));

            }

        }
    }
}
