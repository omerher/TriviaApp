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
using Xamarin.Essentials;

namespace TriviaXamarinApp.Views
{
    class MainPageViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private int score;
        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                if (this.score != value)
                {
                    this.score = value;
                    OnPropertyChanged(nameof(Score));
                }
            }
        }

        private bool enabled;
        public bool Enabled
        {
            get
            {
                return this.enabled;
            }
            set
            {
                if (this.enabled != value)
                {
                    this.enabled = value;
                    OnPropertyChanged(nameof(Enabled));
                }
            }
        }

        private string questionText;
        public string QuestionText
        {
            get
            {
                return this.questionText;
            }
            set
            {
                if (this.questionText != value)
                {
                    this.questionText = value;
                    OnPropertyChanged(nameof(QuestionText));
                }
            }
        }

        private string author;
        public string Author
        {
            get
            {
                return this.author;
            }
            set
            {
                if (this.author != value)
                {
                    this.author = value;
                    OnPropertyChanged(nameof(Author));
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

        public event Action<Page> Push;
        public event Action Reset;

        public ICommand ProfileCommand => new Command(() => Push.Invoke(new ProfilePage()));
        public ICommand StartPlaying => new Command(Start);
        private async void Start()
        {
            try
            {
                string email = await SecureStorage.GetAsync("email");
                string password = await SecureStorage.GetAsync("password");

                if (email != "" && password != "")
                {
                    TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
                    User user = await proxy.LoginAsync(email, password);
                    if (user != null)
                        ((App)App.Current).currentUser = user;
                }
            }
            catch (Exception ex)
            {
                // Possible that device doesn't support secure storage on device.
            }
            await GetQuestion();
        }

        private async Task GetQuestion()
        {
            await Task.Delay(1000);
            Reset.Invoke();
            TriviaWebAPIProxy proxy = TriviaWebAPIProxy.CreateProxy();
            AmericanQuestion americanQuestion = await proxy.GetRandomQuestion();

            QuestionText = americanQuestion.QText;
            Author = americanQuestion.CreatorNickName;
            CorrectAnswer = americanQuestion.CorrectAnswer;
            OtherAnswers = new string[4];
            for (int i = 0; i < 3; i++)
                OtherAnswers[i] = americanQuestion.OtherAnswers[i];
            OtherAnswers[3] = CorrectAnswer;

            Random rnd = new Random();
            OtherAnswers = OtherAnswers.OrderBy(x => rnd.Next()).ToArray();
        }

        public delegate Task<bool> PopupDelegate();
        public event PopupDelegate Popup;

        public delegate void ColorsDelegate(string correct, string wrong);
        public event ColorsDelegate Colors;

        public ICommand SelectedCommand => new Command<string>(Selected);
        private async void Selected(string answer)
        {
            int selectedAnswer = int.Parse(answer);
            int correctAnswerIndex = Array.FindIndex(OtherAnswers, a => a == CorrectAnswer);
            if (selectedAnswer == correctAnswerIndex)
            {
                Colors.Invoke(answer, "");
                Score++;

                if (Score % 3 == 0)
                {
                    bool yesOrNo = await Popup();
                    if (yesOrNo)
                        Score = 0;
                }
            }
            else
            {
                Colors.Invoke("" + correctAnswerIndex, answer);
            }
            await GetQuestion();
        }

        public MainPageViewModel()
        {
            Start();
        }
    }
}
