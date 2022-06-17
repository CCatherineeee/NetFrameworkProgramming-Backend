using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HandyShare.DTO;
using HandyShare.EmailHandler;
using HandyShare.Models;
using HandyShare.Response;
using HandyShare.Service;
using HandyShareOssStorageCLI;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetFrameBackend.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {


        private readonly string prefix = "https://handyshare-1308588633.cos.ap-shanghai.myqcloud.com/";


        [HttpGet("CreateVerification")]
        public IActionResult SendEmailTest(String mailAddress)
        {
            EmailHandler email = new EmailHandler();
            string verifyCode = "1234";
            string content = "以下是您的验证码，请妥善保管：\n" + verifyCode;
            email.Sendmail("来自lxy的邮件", content, mailAddress);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserDTO userdto)
        {
            if(userdto.verifyCode == "1234")
            {
                var flag = await UserService.IsEmailRegistered(userdto.Email);
                if (flag == true)
                {
                    return Ok(new IResponse(400, null, "此邮箱已被注册！"));
                }
                User user = new User();
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.Name = userdto.Name;
                UserService.AddUser(user);
                return Ok(new IResponse(200, null, "注册成功！"));
            }
            else
            {
                return Ok(new IResponse(400, null, "验证码错误！"));
            }

        }


        [HttpPost("passwordLogin")]
        public async Task<IActionResult> passwordLogin(UserDTO userdto)
        {
            var flag = await UserService.IsUserExistByName(userdto.Name);
            if (!flag)
            {
                return Ok(new IResponse(400, null, "用户不存在！"));
            }
            var id = await UserService.CheckPasswordByName(userdto.Name, userdto.Password);
            if (id != -1)
            {
                return Ok(new IResponse(200, id , "登陆成功!"));
            }
            return Ok(new IResponse(400, null, "密码错误！"));
        }


        [HttpPost("emailLogin")]
        public async Task<IActionResult> emailLogin(UserDTO userdto)
        {
            var flag = await UserService.IsUserExistByEmail(userdto.Email);
            if (flag == false)
            {
                return Ok(new IResponse(400, null, "用户不存在！"));
            }
            var id = await UserService.CheckPasswordByEmail(userdto.Email, userdto.Password);
            if(id != -1)
            {
                return Ok(new IResponse(200, id, "登陆成功!"));
            }
            return Ok(new IResponse(400, null, "密码错误！"));
        }

        [HttpGet("resetPasswordGetVerify")]
        public IActionResult resetPasswordGetVerify(string mailAddress)
        {
            EmailHandler email = new EmailHandler();
            string verifyCode = "1234";
            string content = "以下是您的验证码，请妥善保管：\n" + verifyCode;
            email.Sendmail("重置密码验证码", content, mailAddress);
            return Ok();
        }

        [HttpPost("resetPassword")]
        public async Task<IActionResult> resetPassword(UserDTO userDTO)
        {
            if (userDTO.verifyCode != "1234")
            {
                return Ok(new IResponse(400, null, "验证码错误！"));
            }
            try
            {
                await UserService.ResetPassword(userDTO.Email, userDTO.Password);
            }
            catch
            {
                return Ok(new IResponse(400, null, "修改失败！"));
            }
            return Ok(new IResponse(200, null, "修改成功！"));

        }

        [HttpPost("UploadPic")]
        public async Task<IOssResponse> UploadPic([FromForm(Name = "file")] List<IFormFile> files)
        {
            List<string> urlList = new List<string>();
            files.ForEach(async file =>
            {
                String str = OssStorage.GenerateName()+"user";
                var url = "";
                var fileName = file.FileName;
                var stream = file.OpenReadStream();
                byte[] bytes = new byte[stream.Length];
                stream.Read(bytes, 0, bytes.Length);
                await PostService.UploadPic(str + fileName, bytes);
                url = prefix + str + fileName;
                urlList.Add(url);
            });
            IOssResponse res = new IOssResponse();
            res.errno = 0;
            res.data = new IOssResponseData();
            res.data.url = urlList[0];

            return res;
        }


        [HttpGet("getById")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await UserService.GetById(id);
            if(user == null)
            {
                return Ok(new IResponse(404, null, "用户不存在！"));
            }
            return Ok(new IResponse(200, user, "获得成功！"));

        }


        [HttpPost("setProfile")]
        public async Task<IActionResult> SetProfile(UserDTO userDTO)
        {
            var flag = await UserService.ResetUser(userDTO);
            if (flag)
            {
                return Ok(new IResponse(200, null, "修改成功！"));

            }
            return Ok(new IResponse(500,null, "用户不存在！"));


        }

        [HttpGet("getFollow")]
        public async Task<IActionResult> GetFollow(int id)
        {
            var obj = await UserService.GetFollow(id);
            if (obj == null)
            {
                return Ok(new IResponse(404, null, "用户不存在！"));
            }
            return Ok(new IResponse(200, obj, "获得成功！"));
        }

    }
}
