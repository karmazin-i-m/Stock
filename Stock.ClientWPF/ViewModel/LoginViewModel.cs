using Stock.ClientWPF.Commands;
using Stock.ClientWPF.Interfaces;
using Stock.ClientWPF.Model;
using Stock.ClientWPF.Navigator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using Newtonsoft.Json;
using System.Web;
using System.Net;
using System.IO;

namespace Stock.ClientWPF.ViewModel
{
    class LoginViewModel : BaseViewModel
    {
        private String login = "illya_ua";
        private String password = "qwerty";

        public String Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged("Login");
            }
        }
        public String Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged("Password");
            }
        }

        public static readonly string HomeViewModelAlias = "HomePageVM";
        private readonly IViewModelsResolver _resolver;
        private readonly INotifyPropertyChanged homePageViewModel;

        private ICommand goToHomePgeCommand;

        public ICommand GoToHomePgeCommand
        {
            get
            {
                return goToHomePgeCommand ?? new RelayCommand<INotifyPropertyChanged>(async (INotifyPropertyChanged) =>
                {
                    LoginModel loginModel = new LoginModel() { Username = Login, Password = Password };
                    HttpClient client = new HttpClient();


                    String json = JsonConvert.SerializeObject(loginModel, Formatting.Indented);

                    await PostRequestAsync(loginModel);

                    Navigation.Navigate(Navigation.HomePageAlias, HomePageViewModel);
                });
            }
        }

        public INotifyPropertyChanged HomePageViewModel
        {
            get { return homePageViewModel; }
        }

        public LoginViewModel()
        {
            _resolver = new ViewModelsResolver();

            homePageViewModel = _resolver.GetViewModelInstance(HomeViewModelAlias);

            //InitializeCommands();
        }

        //private void InitializeCommands()
        //{
        //    GoToHomePgeCommand = new RelayCommand<INotifyPropertyChanged>((INotifyPropertyChanged) =>
        //    {

        //    });
        //}

        private static async Task PostRequestAsync(LoginModel loginModel)
        {
            WebRequest request = WebRequest.Create("http://localhost:20895/api/authorization");
            request.Method = "POST"; // для отправки используется метод Post
                                     // данные для отправки
            string data = JsonConvert.SerializeObject(loginModel, Formatting.Indented);
            // преобразуем данные в массив байтов
            byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(data);
            // устанавливаем тип содержимого - параметр ContentType
            request.ContentType = "application/json";
            // Устанавливаем заголовок Content-Length запроса - свойство ContentLength
            request.ContentLength = byteArray.Length;

            //записываем данные в поток запроса
            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
            }

            WebResponse response = await request.GetResponseAsync();
            using (Stream stream = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    MessageBox.Show(reader.ReadToEnd());
                }
            }
            response.Close();
        }
    }
}
