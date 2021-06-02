using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainTabbed : TabbedPage
    {
        public MainTabbed()
        {
            NavigationPage mainPage = new NavigationPage(new MainPage());
            mainPage.Title = "Play";
            NavigationPage profilePage = new NavigationPage(new ProfilePage());
            profilePage.Title = "Profile";

            Children.Add(mainPage);
            Children.Add(profilePage);

            InitializeComponent();
        }
    }
}