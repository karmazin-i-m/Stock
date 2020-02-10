using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stock.ClientWPF.Navigator
{
    /// <summary>
    /// класс хранящий данные о всех страницах (Нужно заменить на что-то адекватное).
    /// </summary>
    public class PagesResolver : IPageResolver
    {
        private static readonly Dictionary<string, Func<Page>> _pagesResolvers = new Dictionary<string, Func<Page>>();
        private static readonly Object _lock = new object();
        private static PagesResolver instance;
        private PagesResolver()
        {
            _pagesResolvers.Add(Navigation.HomePageAlias, () => new View.ProductView.HomePage());
            _pagesResolvers.Add(Navigation.RegistrationPageAlias, () => new RegistrationPage());
            _pagesResolvers.Add(Navigation.UserPageAlias, () => new UserPage());
        }

        public Page GetPageInstance(string alias)
        {
            return _pagesResolvers[alias]();
        }

        public static PagesResolver GetInstance()
        {
            if (instance == null)
            {
                lock (_lock)
                {
                    if (instance == null)
                        instance = new PagesResolver();
                }
            }

            return instance;
        }
    }
}
