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
    public partial class HomePage : ContentPage
    {
        public HomePage()
        {
            HomePageViewModel context = new HomePageViewModel();
            context.GetStarted += GetStart;
            this.BindingContext = context;

            InitializeComponent();
        }

        private void GetStart()
        {
            Navigation.PushAsync(new MainPage());
        }
    }
}