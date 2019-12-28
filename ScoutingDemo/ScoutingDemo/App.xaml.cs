using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutingDemo
{
    public partial class App : Application
    {
        public static int screenHeight, screenWidth;
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage());
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
