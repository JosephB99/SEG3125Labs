using System;
using System.Reflection.Emit;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace Lab1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = false)]
    public class Settings : AppCompatActivity
    {
        public static int NoOfQuestions;
        public static int PassingGrade = 50;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.settingsActivity);

        }

        private void SaveBtnClick(object sender, EventArgs e)
        {

            EditText QuestionCntText = FindViewById<EditText>(Resource.Id.QuestionCountText);
            EditText PassGrdText = FindViewById<EditText>(Resource.Id.PassGradeText);
            int EditTxt1 = int.Parse(QuestionCntText.Text.ToString());
            int EditTxt2 = int.Parse(PassGrdText.Text.ToString());
            if (string.IsNullOrEmpty(QuestionCntText.Text) | string.IsNullOrEmpty(PassGrdText.Text))
            {
                DisplayAlert("Empty Field", "Please fill in the parameters");
            }

            if (EditTxt2 > 100 | EditTxt2 < 0)
            {
                DisplayAlert("Invalid Choice", "The percentage has to be within 0 and 100!");
            }
             
            if (QuestionCntText.Text is string | PassGrdText.Text is string)
            {
                DisplayAlert("Invalid Choice", "The parameter only takes integers!");
            }
           
            NoOfQuestions = int.Parse(QuestionCntText.Text.ToString());
            PassingGrade = int.Parse(PassGrdText.Text.ToString());

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
    }
}