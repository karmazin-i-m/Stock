using Stock.ClientWPF.Commands;
using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Stock.ClientWPF.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private String login;
        private String password;

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

        public static readonly string HomeViewModelAlias = "HomePageVM";
        private readonly IViewModelsResolver _resolver;
        private readonly INotifyPropertyChanged homePageViewModel;

        private ICommand goToHomePgeCommand;

        public ICommand GoToHomePgeCommand
        {
            get
            {
                return goToHomePgeCommand ?? new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                {
                    LoginModel loginModel = new LoginModel() { Username = Login, Password = Password };
                    HttpClient client = new HttpClient();

                    
                    var json = serializer.Serialize(new { id = 123, sum = new { amount = 0, currency = 643 } });
                    var response = request.Post(address, json, contentType);

                    using (var stream = client.GetAsync("http://localhost:20895/api/authorization"))
                    {
                        String html = stream.Result.Content.ReadAsStringAsync().Result;
                        MessageBox.Show(html+ $"{loginModel.Username} {loginModel.Password}");
                    }

                    Navigation.Navigate(Navigation.HomePageAlias, HomePageViewModel);
                });
            }
        }

        public INotifyPropertyChanged HomePageViewModel
        {
            get { return homePageViewModel; }
        }

        public LoginViewModel()
        {
            _resolver = new ViewModelsResolver();

            homePageViewModel = _resolver.GetViewModelInstance(HomeViewModelAlias);

            //InitializeCommands();
        }

        //private void InitializeCommands()
        //{
        //    GoToHomePgeCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
        //    {
               
        //    });
        //}
    }
}
