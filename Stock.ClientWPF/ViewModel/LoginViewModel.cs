using Stock.ClientWPF.Commands;
using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
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
    class LoginViewModel : BaseViewModel
    {
        private String login;
        private String password;
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
        public String PageSourse
        {
            get { return pageSourse; }
            set
            {
                pageSourse = value;
                OnPropertyChanged("PageSourse");
            }
        }

        public static readonly string HomeViewModelAlias = "HomePageVM";
        private readonly IViewModelsResolver _resolver;
        private readonly INotifyPropertyChanged homePageViewModel;

        private ICommand _goToHomePgeCommand;

        public ICommand GoToHomePgeCommand
        {
            get
            {
                return _goToHomePgeCommand;
            }
            set
            {
                _goToHomePgeCommand = value;
                OnPropertyChanged("GoToHomePgeCommand");
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

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GoToHomePgeCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
            {
                Navigation.Navigate(Navigation.HomePageAlias, HomePageViewModel);
            });
        }
    }
}

//private ICommand loginCommand;
//public RelayCommand LoginCommand
//{
//    get
//    {
//        return loginCommand ??
//            (loginCommand = new RelayCommand(obj =>
//            {
//                LoginModel loginModel = new LoginModel() { Username = Login, Password = Password};
//                PageSourse = "LoginPage.xaml";
//            }));
//    }
//}
