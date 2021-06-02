using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            LoginPageViewModel context = new LoginPageViewModel();
            context.GoMain += GoToMain;
            this.BindingContext = context;

            InitializeComponent();
        }

        private void GoToMain()
        {
            Navigation.PopToRootAsync();
        }

        private void SignUp(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }
    }
}