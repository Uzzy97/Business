using Business.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Business
{
    public partial class App : Application
    {
        public static INoteEntryStorage Entries { get; set; }

        public App()
        {
            InitializeComponent();
            Entries = new FileEntryStorage();

            MainPage = new NavigationPage(new Business.MainPage())
            {
                BarBackgroundColor = Color.FromHex("#000b3a"),
                BarTextColor = Color.LightSlateGray
            };
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
