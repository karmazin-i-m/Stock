using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Newtonsoft.Json;
using System.Net;
using Stock.ClientWPF.Helpers;

namespace Stock.ClientWPF.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private String login = "illya_ua";
        private String password = "qwerty";

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
        public static readonly string RegistrationViewModelAlias = "RegistrationPageVM";

        private readonly IViewModelsResolver _resolver;

        private readonly INotifyPropertyChanged homePageViewModel;
        private readonly INotifyPropertyChanged registrationPageViewModel;

        private ICommand goToHomePageCommand;
        private ICommand goToRegistrationPageCommand;

        public ICommand GoToHomePageCommand
        {
            get
            {
                return goToHomePageCommand ?? new RelayCommand<INotifyPropertyChanged>(async (INotifyPropertyChanged) =>
                {
                    LoginModel loginModel = new LoginModel() { Username = Login, Password = Password };

                    String user;
                    try
                    {
                        user = await WebHelper.PostRequestAsync("http://localhost:20895/api/authorization", loginModel);
                    }
                    catch (WebException e)
                    {
                        MessageBox.Show(e.Message);
                        return;
                    }
                    UserModel.CurrentUser = JsonConvert.DeserializeObject<UserModel>(user);

                    String products;
                    try
                    {
                        products = await WebHelper.GetRequestAsync("http://localhost:20895/api/products", token: UserModel.CurrentUser.Token);
                        ProductModel.Products = JsonConvert.DeserializeObject<IEnumerable<ProductModel>>(products);
                    }
                    catch (WebException e)
                    {
                        MessageBox.Show(e.Message + e.Status +e.InnerException?.Message);
                        return;
                    }

                    Navigation.Navigate(Navigation.HomePageAlias, HomePageViewModel);
                });
            }
            set
            {
                goToHomePageCommand = value;
                OnPropertyChanged("GoToHomePgeCommand");
            }
        }

        public ICommand GoToRegistrationPageCommand
        {
            get
            {
                return goToRegistrationPageCommand ?? new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                {
                    Navigation.Navigate(Navigation.RegistrationPageAlias, RegistrationPageViewModel);
                });
            }
            set
            {
                goToRegistrationPageCommand = value;
                OnPropertyChanged("GoToRegistrationPageCommand");
            }
        }

        public INotifyPropertyChanged HomePageViewModel
        {
            get { return homePageViewModel; }
        }

        public INotifyPropertyChanged RegistrationPageViewModel
        {
            get { return registrationPageViewModel; }
        }

        public LoginViewModel()
        {
            _resolver = ViewModelsResolver.GetInstance();

            homePageViewModel = _resolver.GetViewModelInstance(HomeViewModelAlias);
            registrationPageViewModel = _resolver.GetViewModelInstance(RegistrationViewModelAlias);
        }
    }
}
