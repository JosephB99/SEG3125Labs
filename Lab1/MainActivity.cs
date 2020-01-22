using System;
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
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            JSONHelper.LoadJson(this);

            //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1); 
            ListView testSelection = FindViewById<ListView>(Resource.Id.listViewtest);
            PopulateTests();
            testSelection.ItemClick += TestSelection_ItemClick;    
            //spinner.SetSelection(0, false);
            //spinner.ItemSelected += Spinner_ItemSelected;
            
        }

        private void TestSelection_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var testId = JSONHelper.TestsList[e.Position].Id;

            Intent elem1 = new Intent(this, typeof(Questionnaire));
            StartActivity(elem1);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Replace with your own action", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void PopulateTests()
        {
            //Spinner spinner = FindViewById<Spinner>(Resource.Id.spinner1);
            ListView listView = FindViewById<ListView>(Resource.Id.listViewtest);

            ArrayAdapter<Tests> testAdapter = new ArrayAdapter<Tests>(this, Android.Resource.Layout.SimpleListItem1);
            
            

            foreach (Tests test in JSONHelper.TestsList)
            {
                testAdapter.Add(test.Name);
            }

            //testAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);

            listView.Adapter = testAdapter;
        }

      
    }
}

