using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HandyShare.DTO;
using HandyShare.EmailHandler;
using HandyShare.Model;
using HandyShare.Response;
using HandyShare.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFrameBackend.Utils;

namespace NetFrameBackend.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {

/*
        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }*/

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
                    return Ok(new Response(400, null, "此邮箱已被注册！"));
                }
                User user = new User();
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.Name = userdto.Name;
                UserService.AddUser(user);
                return Ok(new Response(200, null, "注册成功！"));
            }
            else
            {
                return Ok(new Response(400, null, "验证码错误！"));
            }

        }


        [HttpPost("passwordLogin")]
        public async Task<IActionResult> passwordLogin(UserDTO userdto)
        {
            var flag = await UserService.IsUserExistByName(userdto.Name);
            if (!flag)
            {
                return Ok(new Response(400, null, "用户不存在！"));
            }
            var id = await UserService.CheckPasswordByName(userdto.Name, userdto.Password);
            if (id != -1)
            {
                return Ok(new Response(200, id , "登陆成功!"));
            }
            return Ok(new Response(400, null, "密码错误！"));
        }


        [HttpPost("emailLogin")]
        public async Task<IActionResult> emailLogin(UserDTO userdto)
        {
            var flag = await UserService.IsUserExistByEmail(userdto.Email);
            if (flag == false)
            {
                return Ok(new Response(400, null, "用户不存在！"));
            }
            var id = await UserService.CheckPasswordByEmail(userdto.Email, userdto.Password);
            if(id != -1)
            {
                return Ok(new Response(200, id, "登陆成功!"));
            }
            return Ok(new Response(400, null, "密码错误！"));
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
                return Ok(new Response(400, null, "验证码错误！"));
            }
            try
            {
                await UserService.ResetPassword(userDTO.Email, userDTO.Password);
            }
            catch
            {
                return Ok(new Response(400, null, "修改失败！"));
            }
            return Ok(new Response(200, null, "修改成功！"));

        }

/*        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }*/

    }
}
