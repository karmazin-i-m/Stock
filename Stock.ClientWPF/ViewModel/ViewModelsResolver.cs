using Stock.ClientWPF.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.ClientWPF.ViewModel
{
    /// <summary>
    /// Класс реализующий коллекцию всех вьюмоделей (Нужно заменить на что-то адекватное).
    /// </summary>
    public class ViewModelsResolver : IViewModelsResolver
    {
        private static readonly Dictionary<string, Func<INotifyPropertyChanged>> _vmResolvers = new Dictionary<string, Func<INotifyPropertyChanged>>();
        private static volatile ViewModelsResolver instance;
        private static readonly Object _lock = new object();

        private ViewModelsResolver()
        {
            _vmResolvers.Add(LoginViewModel.HomeViewModelAlias, () => new HomeViewModel());
            _vmResolvers.Add(LoginViewModel.RegistrationViewModelAlias, () => new RegistrationViewModel());
            _vmResolvers.Add(HomeViewModel.UserViewModelAlias, () => new UserViewModel());
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
