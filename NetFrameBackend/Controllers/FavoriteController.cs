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
    }
}
