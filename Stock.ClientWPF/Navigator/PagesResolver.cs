﻿using Stock.ClientWPF.Interfaces;
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
        private static readonly Object _lock = new object();
        private static PagesResolver instance;
        private PagesResolver()
        {
            _pagesResolvers.Add(Navigation.HomePageAlias, () => new HomePage());
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
