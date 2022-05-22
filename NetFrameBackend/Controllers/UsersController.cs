﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetFrameBackend.DTO;
using NetFrameBackend.Models;
using NetFrameBackend.Utils;

namespace NetFrameBackend.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly netContext _context;

        public UsersController(netContext context)
        {
            _context = context;
        }



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
        }

        [HttpGet("CreateVerification")]
        public IActionResult SendEmailTest(String mailAddress)
        {
            Email email = new Email();
            string verifyCode = "1234";
            string content = "以下是您的验证码，请妥善保管：\n" + verifyCode;
            email.Sendmail("来自lxy的邮件", content, mailAddress);
            return Ok();
        }

        [HttpPost("register")]
        public async Task<ActionResult<User>> register(UserDTO userdto)
        {
            if(userdto.verifyCode == "1234")
            {
                var user_ = await _context.Users.FirstOrDefaultAsync(a => a.Email == userdto.Email);
                if (user_ != null)
                {
                    return BadRequest("此邮箱已被注册");
                }
                User user = new User();
                user.Email = userdto.Email;
                user.Password = userdto.Password;
                user.Name = userdto.Name;
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return CreatedAtAction("registered", new { id = user.UserId });
            }
            else
            {
                return BadRequest("验证码错误");
            }

        }

        // DELETE: api/Users/5
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
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}