using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TriviaXamarinApp.Models;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateQuestionPage : ContentPage
    {
        public CreateQuestionPage()
        {
            CreateQuestionPageViewModel context = new CreateQuestionPageViewModel();
            context.Push += (p) => Navigation.PushAsync(p);
            context.Popup += OnAlertYesNoClicked;
            this.BindingContext = context;
            InitializeComponent();
        }

        async Task<bool> OnAlertYesNoClicked()
        {
            bool answer = await DisplayAlert("Not Logged In!", "Would you like to log in?", "Yes", "No");
            if (answer)
            {
                await Navigation.PushAsync(new CreateQuestionPage());
            }
            return answer;
        }
    }
}