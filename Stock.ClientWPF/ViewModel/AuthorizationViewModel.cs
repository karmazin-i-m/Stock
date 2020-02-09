using Stock.ClientWPF.Commands;
using Stock.ClientWPF.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Stock.ClientWPF.ViewModel
{
    class AuthorizationViewModel : INotifyPropertyChanged
    {
        private String login;
        private String password;
        //private String logo;
        private String pageSourse = "LoginPage.xaml";

        public String Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }
        //public String Logo
        //{
        //    get { return logo; }
        //    set 
        //    {
        //        logo = value;
        //        OnPropertyChanged("Logo");
        //    }
        //}
        public String PageSourse
        {
            get { return pageSourse; }
            set
            {
                pageSourse = value;
                OnPropertyChanged("PageSourse");
            }
        }

        private RelayCommand loginCommand;
        public RelayCommand LoginCommand
        {
            get
            {
                return loginCommand ??
                    (loginCommand = new RelayCommand(obj =>
                    {
                        LoginModel loginModel = new LoginModel() { Username = Login, Password = Password};
                        PageSourse = "LoginPage.xaml";
                        
                    }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
