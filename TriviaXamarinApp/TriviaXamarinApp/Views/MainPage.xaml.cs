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
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            MainPageViewModel context = new MainPageViewModel();
            context.Push += (p) => Navigation.PushAsync(p);
            context.Popup += OnAlertYesNoClicked;
            context.Colors += Answered;
            context.Reset += ResetColors;
            
            this.BindingContext = context;

            InitializeComponent();
        }

        private void Answered(string correct, string wrong)
        {
            Dictionary<string, Button> buttonDict = new Dictionary<string, Button>();
            buttonDict.Add("0", b1);
            buttonDict.Add("1", b2);
            buttonDict.Add("2", b3);
            buttonDict.Add("3", b4);

            buttonDict[correct].BackgroundColor = Color.FromHex("1A9B20");
            if (wrong != "")
                buttonDict[wrong].BackgroundColor = Color.FromHex("F50F24");
        }

        private void ResetColors()
        {
            b1.BackgroundColor = (Color)Application.Current.Resources["menu"];
            b2.BackgroundColor = (Color)Application.Current.Resources["menu"];
            b3.BackgroundColor = (Color)Application.Current.Resources["menu"];
            b4.BackgroundColor = (Color)Application.Current.Resources["menu"];
        }

        async Task<bool> OnAlertYesNoClicked()
        {
            bool answer = await DisplayAlert("Question?", "Would you like to add a question", "Yes", "No");
            if (answer)
            {
                await Navigation.PushAsync(new CreateQuestionPage());
            }
            return answer;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateQuestionPage());
        }

        private void Profile_Click(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfilePage());
        }
    }
}