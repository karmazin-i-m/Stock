using Stock.ClientWPF.Helpers;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stock.ClientWPF.ViewModel
{
    class UserViewModel : BaseViewModel
    {
        private Int32 id;
        private String name;
        private String login;
        private String email;
        private Role role;

        private ICommand goToHomePageCommand;

        public ICommand GoToHomePageCommand
        {
            get
            {
                return goToHomePageCommand ??
                    (goToHomePageCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                    {
                            Navigation.GoBack();
                    }));
            }
        }
        public INotifyPropertyChanged BasePageViewModel
        {
            get { return new BaseViewModel(); }
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
