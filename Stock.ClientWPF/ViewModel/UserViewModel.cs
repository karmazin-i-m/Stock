﻿using Stock.ClientWPF.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.ViewModel
{
    class UserViewModel : BaseViewModel
    {
        private Int32 id;
        private String name;
        private String login;
        private String email;
        private Role role;

        public UserViewModel()
        {
            
        }
        public Int32 Id
        {
            get { return id != default ? id : (id = UserModel.CurrentUser.Id); }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        public String Name
        {
            get { return name != default ? name : (name = UserModel.CurrentUser.Name); }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Login
        {
            get { return login != default ? login : (login = UserModel.CurrentUser.Name); }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public String Email
        {
            get { return email != default ? email : (email = UserModel.CurrentUser.Email); }
            set
            {
                email = value;
                OnPropertyChanged("Email");
            }
        }
        public Role Role
        {
            get { return role != default ? role : (role = UserModel.CurrentUser.Role); }
            set
            {
                role = value;
                OnPropertyChanged("Role");
            }
        }
    }
}
