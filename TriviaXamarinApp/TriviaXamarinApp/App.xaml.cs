using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Services;
using TriviaXamarinApp.Models;
using System.Threading.Tasks;
using TriviaXamarinApp.Views;

namespace TriviaXamarinApp
{
    public partial class App : Application
    {
        public User currentUser;
        public App()
        {
            InitializeComponent();
            Resources.Add("background", Color.FromHex("181818"));
            Resources.Add("menu", Color.FromHex("212121"));
            Resources.Add("hover", Color.FromHex("3D3D3D"));
            Resources.Add("primaryText", Color.FromHex("FFFFFF"));
            Resources.Add("secondaryText", Color.FromHex("AAAAAA"));

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
