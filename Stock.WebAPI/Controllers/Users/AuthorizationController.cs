using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Stock.DB.Repositories;
using Stock.DB.Models;
using Stock.DB;
using Stock.WebAPI.Models;

namespace Stock.WebAPI.Users.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly UnitOfWork db;
        private readonly IRepository<User> users;

        public AuthorizationController(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.db = new UnitOfWork();
            users = db.Users;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            var user = Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<String> Get()
        {
            return "hello world";
        }

        private string BuildToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
              configuration["Jwt:Issuer"],
              expires: DateTime.Now.AddMinutes(30),
              signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private User Authenticate(LoginModel login)
        {
            IEnumerable<User> allUsers = this.users.GetList();

            User user = allUsers.FirstOrDefault((currentUser) => 
            {
                if (currentUser.Login == login.Username && currentUser.Password == login.Password)
                    return true;
                return false;
            });

            return user;
        }
    }
}