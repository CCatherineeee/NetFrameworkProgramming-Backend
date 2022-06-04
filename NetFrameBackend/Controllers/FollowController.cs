using HandyShare.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HandyShare.Service
{
    [Route("api/Follow")]
    [ApiController]
    public class FollowController : ControllerBase
    {
        [HttpPost("disfollow")]
        public async Task<IActionResult> Disfollow(int uid,int toid)
        {
            var flag = await FollowService.Disfollow(uid, toid);
            if (flag)
            {
                return Ok(new IResponse(200, null, "取消关注！"));

            }
            return Ok(new IResponse(500, null, "发送错误！"));
        }


    }
}
