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

namespace Lab1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class Summary : AppCompatActivity
    {
        private double FinalScore;
        private int NumOfQuestions;
        private int NumCorrect;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.summary);

            Button btnHome = FindViewById<Button>(Resource.Id.BtnHome);
            btnHome.Click += BtnHome_Click;

            NumCorrect = Intent.GetIntExtra("CORRECT_ANSWERS", 0);
            NumOfQuestions = Intent.GetIntExtra("NUM_OF_QUESTIONS", 0);
            FinalScore = (double)NumCorrect / NumOfQuestions;
            FinalScore *= 100;
            GetResults();
        }

        private void BtnHome_Click(object sender, EventArgs e)
        {
            Intent mainPage = new Intent(this, typeof(MainActivity));
            StartActivity(mainPage);
        }

        private void GetResults()
        {

            TextView textNumCorrect = FindViewById<TextView>(Resource.Id.TxtNumCorrect);
            textNumCorrect.Text = NumCorrect.ToString();

            TextView textNumQuestions = FindViewById<TextView>(Resource.Id.TxtNumQuestions);
            textNumQuestions.Text = NumOfQuestions.ToString();

            TextView textPassingGrade = FindViewById<TextView>(Resource.Id.TxtPassingGrade);
            textPassingGrade.Text = Settings.PassingGrade.ToString() + "%";

            TextView textTotal = FindViewById<TextView>(Resource.Id.TxtTotalScore);
            textTotal.Text = FinalScore.ToString() + "%";

            TextView textPassOrFail = FindViewById<TextView>(Resource.Id.TxtPassOrFail);
            textPassOrFail.Text = (FinalScore >= Settings.PassingGrade) ? "Pass" : "Fail";

            TextView textMessage = FindViewById<TextView>(Resource.Id.TxtMessage);
            textMessage.Text = (FinalScore >= Settings.PassingGrade) ? "Congratulations, you did great!" : "Great effort, better luck next time.";

        }   
    }
}