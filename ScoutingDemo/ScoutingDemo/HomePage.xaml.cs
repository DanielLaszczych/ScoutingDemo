using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ScoutingDemo
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class HomePage : ContentPage
    {

        public static PreScout preScout;
        public static AutoScout autoScout;
        public static Data data;

        public HomePage()
        {
            InitializeComponent();
            InitializeControl();
            btnStart.HeightRequest = (App.screenHeight * 10) / 100;
            btnStart.WidthRequest = (App.screenWidth * 50) / 100;
            btnStart.FontSize = (App.screenWidth * 5) / 100;
        }

        private void InitializeControl()
        {
            btnStart.Clicked += async (s, e) =>
            {
                data = new Data();
                preScout = new PreScout();
                autoScout = new AutoScout();
                await Navigation.PushAsync(preScout, true);
            };
        }
    }
}    
