using Stock.ClientWPF.Commands;
using Stock.ClientWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Stock.ClientWPF.Navigator;



namespace Stock.ClientWPF.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public static readonly string Page1ViewModelAlias = "Page1VM";
        public static readonly string Page2ViewModelAlias = "Page2VM";

        private readonly IViewModelsResolver _resolver;


        private ICommand _goToPage1Command;

        private ICommand _goToPage2Command;

        private readonly INotifyPropertyChanged _p1ViewModel;
        private readonly INotifyPropertyChanged _p2ViewModel;

        public ICommand GoToPage1Command
        {
            get
            {
                return _goToPage1Command;
            }
            set
            {
                _goToPage1Command = value;
                OnPropertyChanged("GoToPage1Command");
            }
        }

        public ICommand GoToPage2Command
        {
            get { return _goToPage2Command; }
            set
            {
                _goToPage2Command = value;
                OnPropertyChanged("GoToPage2Command");
            }
        }

        public INotifyPropertyChanged Page1ViewModel
        {
            get { return _p1ViewModel; }
        }

        public INotifyPropertyChanged Page2ViewModel
        {
            get { return _p2ViewModel; }
        }

        public MainViewModel(IViewModelsResolver resolver)
        {
            _resolver = resolver;

            //_p1ViewModel = _resolver.GetViewModelInstance(Page1ViewModelAlias);
            //_p2ViewModel = _resolver.GetViewModelInstance(Page2ViewModelAlias);

            InitializeCommands();
        }

        private void InitializeCommands()
        {
            GoToPage1Command = new RelayCommand<INotifyPropertyChanged>(GoToPage1CommandExecute);
        }

        private void GoToPage1CommandExecute(INotifyPropertyChanged viewModel)
        {
            Navigation.Navigate(Navigation.HomePageAlias, Page1ViewModel);
        }
    }
}
