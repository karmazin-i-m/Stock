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
    class RegistrationViewModel :BaseViewModel
    {
        public static readonly string LoginViewModelAlias = "LoginPageVM";

        private ICommand goToLoginPageCommand;

        public ICommand GoToLoginPageCommand
        {
            get
            {
                return goToLoginPageCommand ?? new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
                {

                    MessageBox.Show("121241242");
                    Navigation.Service.GoBack();
                    //Navigation.Navigate(Navigation.LoginPageAlias, LoginPageViewModel);
                });
            }
            set
            {
                goToLoginPageCommand = value;
                OnPropertyChanged("GoToLoginPageCommand");
            }
        }
    }
}
