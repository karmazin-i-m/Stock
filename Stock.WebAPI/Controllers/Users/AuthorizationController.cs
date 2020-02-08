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
using Stock.WebAPI.Models;
using System.Security.Claims;
using Stock.DB.Services;

namespace Stock.WebAPI.Users.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly IUnitOfWork db;
        private readonly IRepository<User> users;

        public AuthorizationController(IConfiguration configuration, IUnitOfWork db)
        {
            this.configuration = configuration;
            this.db = db;
            users = db.Users;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateTokenAsync([FromBody]LoginModel login)
        {
            IActionResult response = Unauthorized();
            User user = await Authenticate(login);

            if (user != null)
            {
                var tokenString = BuildToken(user);
                response = Ok(new { token = tokenString });
            }

            return response;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Get()
        {
            return Content("Hello on my page!");
        }

        private string BuildToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Login),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> Authenticate(LoginModel login)
        {
            IEnumerable<User> allUsers = await this.users.GetListAsync();

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