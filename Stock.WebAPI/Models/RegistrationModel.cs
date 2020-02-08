using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.WebAPI.Models
{
    public class RegistrationModel
    {
        public String Name { get; set; }
        public String Login { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String ConfirmPassword { get; set; }
    }
}
