using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stock.DB.Models;
using Stock.DB.Repositories;
using Stock.DB.Services;
using Stock.WebAPI.Models;

namespace Stock.WebAPI.Controllers.Users
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class RegistrationController : Controller
    {
        private readonly IUnitOfWork db;
        private readonly IRepository<User> users;

        public RegistrationController(IUnitOfWork db)
        {
            this.db = db;
            this.users = db.Users;
        }   
        /// <summary>
        /// ПРинимает запрос на регистрацию пользователя
        /// </summary>
        /// <param name="registrationUser"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegistrationUser([FromBody]RegistrationModel registrationUser)
        {
            if (registrationUser.Password != registrationUser.Password)
                return BadRequest("Пароли не совпадают");
            return Ok(await RegistrationAsync(registrationUser));
        }
        /// <summary>
        /// Регистрирует пользователя в БД
        /// </summary>
        /// <param name="registrationUser"></param>
        /// <returns></returns>
        private async Task<Int32> RegistrationAsync(RegistrationModel registrationUser)
        {
            Int32 answer = await users.CreateAsync(new DB.Models.User()
            {
                Name = registrationUser.Name,
                Password = registrationUser.Password,
                Email = registrationUser.Email,
                Login = registrationUser.Login,
                Role = Role.User
            });
            db.Save();
            return answer;
        }
    }
}