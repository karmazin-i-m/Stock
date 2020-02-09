using Stock.ClientWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.ViewModel
{
    public class ViewModelsResolver : IViewModelsResolver
    {
        private readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();

        public ViewModelsResolver()
        {
            _vmResolvers.Add(MainViewModel.Page1ViewModelAlias, () => new AuthorizationViewModel());
            _vmResolvers.Add(MainViewModel.Page2ViewModelAlias, () => new HomeViewModel());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
                return _vmResolvers[alias]();
        }
    }
}
