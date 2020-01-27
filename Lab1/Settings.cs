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
        private int PassingGrade;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.settingsActivity);

            EditText QuestionCntText = FindViewById<EditText>(Resource.Id.QuestionCountText);
            EditText PassGrdText = FindViewById<EditText>(Resource.Id.PassGradeText);
            EditTxt1 = QuestionCntText.getText().toString();
            EditTxt2 = PassGrdText.getText().toString();
            if (EditTxt1.matches("") & EditTxt2.matches(""))
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
            NoOfQuestions = Int32.Parse(QuestionCntText.getText());
            EditText PassGrdText = FindViewById<EditText>(Resource.Id.PassGradeText);
            PassingGrade = Int32.Parse(PassGrdText.getText().toString());

        }


    }
}