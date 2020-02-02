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
        private Button SaveButton;
        private int NoOfQuestions;
        public static int PassingGrade = 50;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.settingsActivity);

            EditText QuestionCntText = FindViewById<EditText>(Resource.Id.QuestionCountText);
            EditText PassGrdText = FindViewById<EditText>(Resource.Id.PassGradeText);
            string EditTxt1 = QuestionCntText.Text.ToString();
            string EditTxt2 = PassGrdText.Text.ToString();
            if (string.IsNullOrEmpty(EditTxt1) & string.IsNullOrEmpty(EditTxt2))
            {
                return;
            }
            else
            {
                SaveButton = FindViewById<Button>(Resource.Id.SaveBtn);
                SaveButton.Text = "Save";
                SaveButton.Click += SaveBtnClick;
            }
            
        }

        private void SaveBtnClick(object sender, EventArgs e)
        {
            EditText QuestionCntText = FindViewById<EditText>(Resource.Id.QuestionCountText);
            NoOfQuestions = int.Parse(QuestionCntText.Text.ToString());
            EditText PassGrdText = FindViewById<EditText>(Resource.Id.PassGradeText);
            PassingGrade = int.Parse(PassGrdText.Text.ToString());

        }


    }
}