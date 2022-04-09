
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using netBackend.Models;
using netBackend.Utils;

namespace netBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly handy_shareContext _context;

        public UsersController(handy_shareContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [EnableCors("cors")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            Console.WriteLine("get");
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

        [EnableCors("cors")]
        [HttpPost("CreateVerification")]
        public IActionResult CreateVerification()
        {
            //TODO:生成验证码
            Console.WriteLine("CreateVerification");
            return Ok("created");
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> Verify(string verification)
        {
            //TODO: 校验验证码
            if (verification.Equals("123"))
            {
                return Ok("verified");
            }
            return NotFound();
        }

        [EnableCors("cors")]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(User user, string verification)
        {
            //TODO: 校验验证码
            if (!verification.Equals("123"))
            {
                return Forbid();
            }
            if (user.Email == null)
                return Forbid();
            List<User> check = _context.Users.Where(s => s.Email == user.Email).ToList();
            if(check.Count != 0)
                return Conflict();
            user.Password = Security.Encode(user.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [EnableCors("cors")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(User user)
        {

            User user_ = _context.Users.Where(s => s.Email == user.Email).OrderBy(u=>u.Email).Take(1).FirstOrDefault();
            if (user_ == null)
                return Forbid();
            user.Password = Security.Encode(user.Password);
            if (user_.Password == user.Password)
            {
                String token = Token.GenerateToken("User", user_.UserId);
                return Ok(token);
            }

            return BadRequest();
        }

        [EnableCors("cors")]
        [HttpPut("Email")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Email(string newEmail, string verification)
        {
            //TODO: 校验验证码
            if(newEmail==null|| verification == null)
            {
                return BadRequest();
            }
            if (!verification.Equals("123"))
            {
                return BadRequest();
            }
            String token = Request.Headers["Authorization"];
            Console.WriteLine(token);
            Console.WriteLine(token.Substring(7));
            string id;
            id = Security.GetId(token.Substring(7));
            if(id == null)
                return Forbid();
            int userid = int.Parse(id);
            User user_ = _context.Users.Where(s => s.UserId == userid).OrderBy(u => u.Email).Take(1).FirstOrDefault();
            if(user_ == null)
            {
                return Forbid();
            }
            user_.Email = newEmail;
            _context.SaveChanges();

            return Ok();
        }

        [EnableCors("cors")]
        [HttpPut("Password")]
        [Authorize(Policy = "User")]
        public async Task<IActionResult> Password(string newPassword, string verification)
        {
            //TODO: 校验验证码
            if (newPassword == null || verification == null)
            {
                return BadRequest();
            }
            if (!verification.Equals("123"))
            {
                return BadRequest();
            }
            String token = Request.Headers["Authorization"];
            Console.WriteLine(token);
            Console.WriteLine(token.Substring(7));
            string id;
            id = Security.GetId(token.Substring(7));
            if (id == null)
                return Forbid();
            int userid = int.Parse(id);
            User user_ = _context.Users.Where(s => s.UserId == userid).OrderBy(u => u.Email).Take(1).FirstOrDefault();
            if (user_ == null)
            {
                return Forbid();
            }
            user_.Password = newPassword;
            _context.SaveChanges();

            return Ok();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
