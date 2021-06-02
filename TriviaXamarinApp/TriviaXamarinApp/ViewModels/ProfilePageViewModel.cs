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
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp.ViewModels
{
    class ProfilePageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string name;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

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

        private bool loggedIn;
        public bool LoggedIn
        {
            get
            {
                return this.loggedIn;
            }
            set
            {
                if (this.loggedIn != value)
                {
                    this.loggedIn = value;
                    OnPropertyChanged(nameof(LoggedIn));
                }
            }
        }

        private bool loggedOut;
        public bool LoggedOut
        {
            get
            {
                return this.loggedOut;
            }
            set
            {
                if (this.loggedOut != value)
                {
                    this.loggedOut = value;
                    OnPropertyChanged(nameof(LoggedOut));
                }
            }
        }

        public event Action<Page> Push;

        public ICommand LogoutCommand => new Command(Logout);
        private void Logout()
        {
            ((App)App.Current).currentUser = null;
            App.Current.MainPage.Navigation.PopToRootAsync();
        }

        public ICommand EditCommand => new Command<AmericanQuestion>(Edit);
        private void Edit(AmericanQuestion q)
        {
            Push.Invoke(new EditQuestionPage(q));
        }

        public ICommand DeleteCommand => new Command<AmericanQuestion>(Delete);
        private async void Delete(AmericanQuestion q)
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool worked = await proxy.DeleteQuestion(q);
            if (worked)
                Questions.Remove(q);
        }

        public ICommand LoginCommand => new Command(() => Push.Invoke(new LoginPage()));

        public ObservableCollection<AmericanQuestion> Questions { get; set; }

        public ProfilePageViewModel()
        {
            if (((App)App.Current).currentUser != null)
            {
                LoggedIn = true;
                LoggedOut = false;
                User u = ((App)App.Current).currentUser;
                Email = u.Email;
                Name = u.NickName;
                Password = u.Password;
                Questions = new ObservableCollection<AmericanQuestion>(((App)App.Current).currentUser.Questions);
            }
            else
            {
                LoggedIn = false;
                LoggedOut = true;
            }
        }
    }
}
