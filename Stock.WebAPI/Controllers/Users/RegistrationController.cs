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
    public class RegistrationController : Controller
    {
        private readonly UnitOfWork db;
        private readonly IRepository<User> users;

        public RegistrationController()
        {
            this.db = UnitOfWork.GetInstance();
            this.users = db.Users;
        }   

        [AllowAnonymous]
        [HttpPost]
        public IActionResult RegistrationYser([FromBody]RegistrationModel registrationUser)
        {
            if (registrationUser.Password != registrationUser.Password)
                return BadRequest("Пароли не совпадают");
            return Ok();
        }

        private void Registration(RegistrationModel registrationUser)
        {
            users.Create(new DB.Models.User()
            {
                Name = registrationUser.Name,
                Password = registrationUser.Password,
                Email = registrationUser.Email,
                Login = registrationUser.Login,
                Role = Role.User
            });
            db.Save();
        }
    }
}