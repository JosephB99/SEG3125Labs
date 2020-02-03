using System;
using Android.App;
using Android.Content;
using Android.OS;
using System.Linq;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lab1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class Questionnaire : AppCompatActivity
    {
        private int TestID;
        private bool TestCompleted = false;
        private Button NextButton;
        private List<Choices> TestChoices;
        private List<Questions> TestQuestions;
        private int QuestionID;
        private int CorrectAnswers;
        protected static double FinalScore;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.questionaire);

            TestID = Intent.GetIntExtra("TESTID", 0);
            TestQuestions = JSONHelper.QuestionsList.FindAll(x => x.TestId.Equals(TestID));

            if (Settings.NoOfQuestions > 0  && Settings.NoOfQuestions < TestQuestions.Count) 
            {
                TestQuestions = TestQuestions.GetRange(0, Settings.NoOfQuestions);
            }

            if (TestQuestions.Count == 0)
            {
                DisplayAlert("No questions", "The selected test has no questions associated!", true);
                return;
            }

            NextButton = FindViewById<Button>(Resource.Id.BtnNextQuestion);
            NextButton.Text = "Next Question";
            NextButton.Click += NextButton_Click;

            InitTitle();
            UpdateAll();
        }

        private async void NextButton_Click(object sender, EventArgs e)
        {
            RadioGroup radioGroupChoices = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            var selectedChoice = radioGroupChoices.CheckedRadioButtonId;


            if (selectedChoice == -1)
            {
                DisplayAlert("Selection Required", "Please select an answer before continuing!");
            }
            else
            {
                Choices correctAnswer = TestChoices.Find(x => x.Correct.Equals(true));
                TextView correctText = FindViewById<TextView>(Resource.Id.TxtCorrect);

                if (correctAnswer.Id == selectedChoice)
                {
                    CorrectAnswers++;
                    correctText.Text = "Correct!"; 
                } else
                {
                    correctText.Text = "Incorrect, the correct answer is " + correctAnswer.Body;
                }

                NextButton.Enabled = false;
                await PutTaskDelay();

                if (TestCompleted)
                {
                    Intent summaryPage = new Intent(this, typeof(Summary));
                    summaryPage.PutExtra("CORRECT_ANSWERS", CorrectAnswers);
                    summaryPage.PutExtra("NUM_OF_QUESTIONS", TestQuestions.Count);
                    StartActivity(summaryPage);
                    return;
                }

                UpdateAll();
            }
        }

        private void InitTitle()
        {
            TextView testTitleText = FindViewById<TextView>(Resource.Id.TxtTestTitle);
            testTitleText.Text = JSONHelper.TestsList[TestID - 1].Name;
            QuestionID = JSONHelper.QuestionsList.Where(x => x.TestId.Equals(TestID)).FirstOrDefault().Id;
        }

        private void UpdateAll()
        {
            if (QuestionID == TestQuestions.FindLast(x => (x.TestId.Equals(TestID))).Id)
            {
                NextButton.Text = "Submit";
                TestCompleted = true;
            }
            TextView questionTitleText = FindViewById<TextView>(Resource.Id.TxtQuestionTitle);
            questionTitleText.Text = JSONHelper.QuestionsList.Find(x => (x.Id.Equals(QuestionID) && x.TestId.Equals(TestID))).Title;
            AddChoices();
            QuestionID++;
            TextView correctText = FindViewById<TextView>(Resource.Id.TxtCorrect);
            correctText.Text = "";
            NextButton.Enabled = true;
        }

        private void AddChoices()
        {
            TestChoices = JSONHelper.ChoicesList.FindAll(x => (x.QuestionId.Equals(QuestionID)));

            RadioGroup radioGroupChoices = FindViewById<RadioGroup>(Resource.Id.radioGroup1);
            radioGroupChoices.ClearCheck();
            radioGroupChoices.RemoveAllViews();

            foreach (var choice in TestChoices)
            {
                RadioButton choiceButton = new RadioButton(this);
                choiceButton.Text = choice.Body;
                choiceButton.Id = choice.Id;
                radioGroupChoices.AddView(choiceButton);
            }
        }


        private void DisplayAlert(string title, string message, bool returnHome = false)
        {
            Android.App.AlertDialog.Builder dialog = new Android.App.AlertDialog.Builder(this);
            Android.App.AlertDialog alert = dialog.Create();
            alert.SetTitle(title);
            alert.SetMessage(message);
            alert.SetButton("OK", (c, ev) =>
            {
                alert.Hide();

                if (returnHome)
                {
                    Intent mainPage = new Intent(this, typeof(MainActivity));
                    StartActivity(mainPage);
                }
            });
            alert.Show();
        }

        private async Task PutTaskDelay() { 
        
            await Task.Delay(2000);
        }

    }
}
