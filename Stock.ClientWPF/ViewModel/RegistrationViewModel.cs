using Stock.ClientWPF.Helpers;
using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Net;

namespace Stock.ClientWPF.ViewModel
{
    class RegistrationViewModel : BaseViewModel
    {
        public static readonly string LoginViewModelAlias = "LoginPageVM";

        private readonly INotifyPropertyChanged basePageViewModel;

        private String name;
        private String login;
        private String email;
        private String password;
        private String confirmPassword;

        private ICommand goToLoginPageCommand;

        public ICommand GoToLoginPageCommand
        {
            get
            {
                return goToLoginPageCommand ??
                    (goToLoginPageCommand = new RelayCommand<INotifyPropertyChanged>(async (INotifyPropertyChanged) =>
                   {
                       RegistrationModel regModel = new RegistrationModel();
                       regModel.Name = Name;
                       regModel.Login = Login;
                       regModel.Email = Email;
                       regModel.Password = Password;
                       regModel.ConfirmPassword = ConfirmPassword;

                       try
                       {
                           MessageBox.Show(await WebHelper.PostRequestAsync("http://localhost:20895/api/registration", regModel));
                       }
                       catch (WebException e)
                       {
                           MessageBox.Show(e.Message);
                           return;
                       }
                       if (Navigation.Service.CanGoBack)
                           Navigation.Service.GoBack();
                   }));
            }
        }

        public INotifyPropertyChanged BasePageViewModel
        {
            get { return new BaseViewModel(); }
        }
        public String Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public String Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public String Email
        {
            get { return email; }
            set
            {
                email = value;
                OnPropertyChanged("Email");
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
        public String ConfirmPassword
        {
            get { return confirmPassword; }
            set
            {
                confirmPassword = value;
                OnPropertyChanged("ConfirmPassword");
            }
        }
    }
}
