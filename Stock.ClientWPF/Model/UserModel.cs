﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.Model
{
    public class UserModel
    {
        public static UserModel CurrentUser { get; set; }

        public String Token { get; set; }
        public Int32 Id { get; set; }
        public String Name { get; set; }
        public String Login { get; set; }
        public String Email { get; set; }
        public Role Role { get; set; }
    }
    public enum Role
    {
        User,
        Admin
    }
}
