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
        private static ViewModelsResolver instance;
        private static readonly Object _lock = new object();

        private ViewModelsResolver()
        {
            _vmResolvers.Add(LoginViewModel.HomeViewModelAlias, () => new HomeViewModel());
        }

        public INotifyPropertyChanged GetViewModelInstance(string alias)
        {
                return _vmResolvers[alias]();
        }

        public static ViewModelsResolver GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new ViewModelsResolver();
                }
            }

            return instance;
        }
    }
}
