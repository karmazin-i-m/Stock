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
    public class PagesResolver : IPageResolver
    {
        private readonly Dictionary<string, Func<Page>> _pagesResolvers = new Dictionary<string, Func<Page>>();

        public PagesResolver()
        {
            _pagesResolvers.Add(Navigation.HomePageAlias, () => new HomePage());
        }

        public Page GetPageInstance(string alias)
        {
            return _pagesResolvers[alias]();
        }
    }
}
