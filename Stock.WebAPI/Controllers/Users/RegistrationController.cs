using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stock.DB;
using Stock.DB.Models;
using Stock.DB.Repositories;
using Stock.WebAPI.Models;

namespace Stock.WebAPI.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly UnitOfWork db;
        private readonly IRepository<User> users;

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegistrationYser([FromBody]RegistrationModel registrationUser)
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
    }
}