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
    class EditQuestionViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public EditQuestionViewModel(AmericanQuestion q)
        {
            QText = q.QText;
            CorrectAnswer = q.CorrectAnswer;
            OtherAnswers = q.OtherAnswers;
            originalQuestion = q;
        }

        private AmericanQuestion originalQuestion;

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

        public ICommand EditCommand => new Command(Edit);
        private async void Edit()
        {
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            bool valid = await proxy.DeleteQuestion(originalQuestion);
            if (valid)
            {
                AmericanQuestion newQuestion = new AmericanQuestion()
                {
                    QText = QText,
                    CorrectAnswer = CorrectAnswer,
                    OtherAnswers = OtherAnswers,
                    CreatorNickName = ((App)App.Current).currentUser.NickName
                };

                if (NotEmpty(newQuestion))
                    await proxy.PostNewQuestion(newQuestion);
            }
        }

        private bool NotEmpty(AmericanQuestion q)
        {
            return (q.QText != "" && q.CorrectAnswer != "" && q.OtherAnswers[0] != "" && q.OtherAnswers[1] != ""
                 && q.OtherAnswers[2] != "" && q.CreatorNickName != "");
        }
    }
}
