using Stock.ClientWPF.Helpers;
using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ProductModel selectedProduct;
        private ObservableCollection<ProductModel> products;

        public ObservableCollection<ProductModel> Products
        {
            get 
            {
                return products != null ? products : (products = new ObservableCollection<ProductModel>(ProductModel.Products));
            }
        }

        public ProductModel SelectedPhone
        {
            get
            {

                return selectedProduct;
            }
            set
            {
                selectedProduct = value;
                OnPropertyChanged("SelectedProduct");
            }
        }

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
