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

namespace TriviaXamarinApp.ViewModels
{
    class RegisterPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public RegisterPageViewModel() { }

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

        private string username;
        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                if (this.username != value)
                {
                    this.username = value;
                    OnPropertyChanged(nameof(Username));
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

        public event Action PopToRoot;

        public ICommand RegisterCommand => new Command(Register);
        private async void Register()
        {
            if (Email != "" && Password != "" && Username != "")
            {
                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                User u = new User()
                {
                    Email = Email,
                    NickName = Username,
                    Password = Password,
                    Questions = new List<AmericanQuestion>()
                };
                bool valid = await proxy.RegisterUser(u);
                if (valid)
                {
                    User user = await proxy.LoginAsync(Email, Password);
                    ((App)App.Current).currentUser = user;
                    PopToRoot.Invoke();
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
