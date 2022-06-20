using AppSpotify.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppSpotify
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            NavigationPage nav = new NavigationPage(new TabbedPage1());
            MainPage = nav;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
