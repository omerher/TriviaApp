using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Linq;
using Xamarin.Essentials;

namespace TriviaXamarinApp.ViewModels
{
    class LoginPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string email;
        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                if (this.email != value)
                {
                    this.email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                if (this.password != value)
                {
                    this.password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        private string errorMessage;
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                if (this.errorMessage != value)
                {
                    this.errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }

        private bool remember;
        public bool Remember
        {
            get
            {
                return this.remember;
            }
            set
            {
                if (this.remember != value)
                {
                    this.remember = value;
                    OnPropertyChanged(nameof(Remember));
                }
            }
        }

        public Action GoMain;
        public ICommand LoginCommand => new Command(Login);
        private async void Login()
        {
            if (Email != "" && Password != "")
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                User user = await proxy.LoginAsync(Email, Password);
                if (user != null)
                {
                    ((App)App.Current).currentUser = user;
                    try
                    {
                        await SecureStorage.SetAsync("email", Email);
                        await SecureStorage.SetAsync("password", Password);
                    }
                    catch (Exception ex)
                    {
                        // Possible that device doesn't support secure storage on device.
                    }

                    GoMain();
                }
                else
                    ErrorMessage = "Something went wrong! Please try again later.";
            }
            else
            {
                ErrorMessage = "Email and password can not be blank!";
            }
        }
    }
}
