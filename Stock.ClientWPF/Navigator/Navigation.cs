﻿using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.View;
using Stock.ClientWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Stock.ClientWPF.Navigator
{
    /// <summary>
    /// Класс совершающий нафигацию по страницам.
    /// </summary>
    public sealed class Navigation
    {

        public static readonly string HomePageAlias = "HomePage";
        public static readonly string RegistrationPageAlias = "RegistrationPage";
        public static readonly string UserPageAlias = "UserPage";

        private readonly Stack<Page> pages;
        private readonly Stack<BaseViewModel> viewModels;

        private NavigationService _navService;
        private readonly IPageResolver _resolver;

        public static NavigationService Service
        {
            get { return Instance._navService; }
            set
            {
                if (Instance._navService != null)
                {
                    Instance._navService.Navigated -= Instance._navService_Navigated;
                }

                Instance._navService = value;
                Instance._navService.Navigated += Instance._navService_Navigated;
            }
        }

        public static void Navigate(Page page, object context)
        {
            if (Instance._navService == null || page == null)
            {
                return;
            }
            Instance.pages.Push(page as Page);
            Instance.viewModels.Push(context as BaseViewModel);

            Instance._navService.Navigate(page, context);
        }
        public static void GoBack()
        {
            if (Instance.pages.Count < 1 | Instance.viewModels.Count < 1)
                throw new Exception("В коллекциях недостаточно элементов");
            Instance.pages.Pop();
            Instance.viewModels.Pop();
            Navigate(Instance.pages.Peek(), Instance.viewModels.Peek());
        }

        public static void Navigate(Page page)
        {
            Navigate(page, null);
        }

        public static void Navigate(string uri, object context)
        {
            if (Instance._navService == null || uri == null)
            {
                return;
            }

            var page = Instance._resolver.GetPageInstance(uri);

            Navigate(page, context);
        }

        public static void Navigate(string uri)
        {
            Navigate(uri, null);
        }

        void _navService_Navigated(object sender, NavigationEventArgs e)
        {
            var page = e.Content as Page;

            if (page == null)
            {
                return;
            }

            page.DataContext = e.ExtraData;
        }

        private static volatile Navigation _instance;
        private static readonly object SyncRoot = new Object();

        private Navigation()
        {
            _resolver = PagesResolver.GetInstance();
            pages = new Stack<Page>();
            viewModels = new Stack<BaseViewModel>();
            pages.Push(new LoginPage());
            viewModels.Push(new LoginViewModel());
        }

        private static Navigation Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new Navigation();
                    }
                }

                return _instance;
            }
        }
    }
}
