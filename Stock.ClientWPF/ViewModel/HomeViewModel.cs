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

namespace Stock.ClientWPF.ViewModel
{
    class HomeViewModel : BaseViewModel
    {
        public static readonly string UserViewModelAlias = "UserPageVM";
        private readonly IViewModelsResolver _resolver;
        private readonly INotifyPropertyChanged userPageViewModel;
        private ICommand goToUserPageCommand;

        public ICommand GoToUserPageCommand
        {
            get
            {
                return goToUserPageCommand ?? (goToUserPageCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                {
                    Navigation.Navigate(Navigation.UserPageAlias, UserPageViewModel);
                }));
            }
        }

        public INotifyPropertyChanged UserPageViewModel
        {
            get { return userPageViewModel; }
        }

        public HomeViewModel()
        {
            _resolver = ViewModelsResolver.GetInstance();

            userPageViewModel = _resolver.GetViewModelInstance(UserViewModelAlias);
        }
    }
}
