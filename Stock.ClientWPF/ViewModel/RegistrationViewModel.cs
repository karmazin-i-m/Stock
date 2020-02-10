using Stock.ClientWPF.Helpers;
using Stock.ClientWPF.Interfaces;
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
    class RegistrationViewModel : BaseViewModel
    {
        public static readonly string LoginViewModelAlias = "LoginPageVM";

        private readonly INotifyPropertyChanged basePageViewModel;

        private ICommand goToLoginPageCommand;

        public ICommand GoToLoginPageCommand
        {
            get
            {
                return goToLoginPageCommand ??
                    (goToLoginPageCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                    {
                        Navigation.Service.GoBack();
                    }));
            }
        }

        public INotifyPropertyChanged BasePageViewModel 
        { 
            get { return new BaseViewModel(); }
        }
    }
}
