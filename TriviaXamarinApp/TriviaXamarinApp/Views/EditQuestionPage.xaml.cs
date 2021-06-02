using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TriviaXamarinApp.Models;
using TriviaXamarinApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TriviaXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditQuestionPage : ContentPage
    {
        public EditQuestionPage(AmericanQuestion q)
        {
            EditQuestionViewModel context = new EditQuestionViewModel(q);
            this.BindingContext = context;
            InitializeComponent();
        }
    }
}