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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            RegisterPageViewModel context = new RegisterPageViewModel();
            context.PopToRoot += () => Navigation.PopToRootAsync();
            this.BindingContext = context;

            InitializeComponent();
        }

        private void Login(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }
    }
}
