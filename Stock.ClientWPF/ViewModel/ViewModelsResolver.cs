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
            _vmResolvers.Add(LoginViewModel.HomeViewModelAlias, () => new HomeViewModel());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
                return _vmResolvers[alias]();
        }
    }
}
