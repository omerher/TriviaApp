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
    class CreateQuestionPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public CreateQuestionPageViewModel()
        {
            OtherAnswers = new string[3];
        }

        private string qText;
        public string QText
        {
            get
            {
                return this.qText;
            }
            set
            {
                if (this.qText != value)
                {
                    this.qText = value;
                    OnPropertyChanged(nameof(QText));
                }
            }
        }

        private string correctAnswer;
        public string CorrectAnswer
        {
            get
            {
                return this.correctAnswer;
            }
            set
            {
                if (this.correctAnswer != value)
                {
                    this.correctAnswer = value;
                    OnPropertyChanged(nameof(CorrectAnswer));
                }
            }
        }

        private string[] otherAnswers;
        public string[] OtherAnswers
        {
            get
            {
                return this.otherAnswers;
            }
            set
            {
                if (this.otherAnswers != value)
                {
                    this.otherAnswers = value;
                    OnPropertyChanged(nameof(OtherAnswers));
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

        public event Action<Page> Push;

        public delegate Task<bool> PopupDelegate();
        public event PopupDelegate Popup;

        public ICommand SubmitCommand => new Command(Submit);
        private async void Submit()
        {
            if (((App)App.Current).currentUser != null)
            {
                AmericanQuestion americanQuestion = new AmericanQuestion()
                {
                    QText = QText,
                    CorrectAnswer = CorrectAnswer,
                    OtherAnswers = OtherAnswers,
                    CreatorNickName = ((App)App.Current).currentUser.NickName
                };

                TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();

                if (NotEmpty(americanQuestion))
                {
                    bool working = await proxy.PostNewQuestion(americanQuestion);
                    if (working)
                    {
                        ((App)App.Current).currentUser.Questions.Add(americanQuestion);
                        Push.Invoke(new MainPage());
                    }
                    else
                        ErrorMessage = "Oops... Something went wrong! Please try again later.";
                }
            }
            else
            {
                bool wantToLogin = await Popup();
                if (wantToLogin)
                {
                    Push.Invoke(new LoginPage());
                }
            }
            
        }

        private bool NotEmpty(AmericanQuestion q)
        {
            return (q.QText != "" && q.CorrectAnswer != "" && q.OtherAnswers[0] != "" && q.OtherAnswers[1] != ""
                 && q.OtherAnswers[2] != "" && q.CreatorNickName != "");
        }

        //private string otherAnswer1;
        //public string OtherAnswer1
        //{
        //    get
        //    {
        //        return this.otherAnswer1;
        //    }
        //    set
        //    {
        //        if (this.otherAnswer1 != value)
        //        {
        //            this.otherAnswer1 = value;
        //            OnPropertyChanged(nameof(OtherAnswer1));
        //        }
        //    }
        //}

        //private string otherAnswer2;
        //public string OtherAnswer2
        //{
        //    get
        //    {
        //        return this.otherAnswer2;
        //    }
        //    set
        //    {
        //        if (this.otherAnswer2 != value)
        //        {
        //            this.otherAnswer2 = value;
        //            OnPropertyChanged(nameof(OtherAnswer2));
        //        }
        //    }
        //}

        //private string otherAnswer3;
        //public string OtherAnswer3
        //{
        //    get
        //    {
        //        return this.otherAnswer3;
        //    }
        //    set
        //    {
        //        if (this.otherAnswer3 != value)
        //        {
        //            this.otherAnswer3 = value;
        //            OnPropertyChanged(nameof(OtherAnswer3));
        //        }
        //    }
        //}
    }
}
