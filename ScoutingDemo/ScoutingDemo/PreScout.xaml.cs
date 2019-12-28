using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ScoutingDemo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PreScout : ContentPage
    {
        private readonly List<String> names = new List<string>
        {
            "Daniel", "Raymond", "Tia",
            "Peter", "Udayan", "Mohammed",
            "Brandon", "Ali", "Potato",
            "RoboTigers", "Cup", "Frank"
        };

        private readonly List<int> teams = new List<int>
        {
            1796, 254, 1114, 195, 125, 123
        };

        private bool hasName;
        private bool hasTeam;
        private bool hasAlliance;
        private bool hasMatch;

        public PreScout()
        {
            InitializeComponent();
            hasName = false;
            hasTeam = false;
            hasAlliance = false;
            hasMatch = false;
            DynamicSizing();
            InitializeControls();
            btnNextPre.Clicked += async (s, e) =>
            {
                HomePage.data.SelectedName = pckName.Items[pckName.SelectedIndex];
                HomePage.data.SelectedAlliance = pckAlliance.Items[pckAlliance.SelectedIndex];
                HomePage.data.SelectedTeam = Int32.Parse(pckTeam.Items[pckTeam.SelectedIndex]);
                HomePage.data.MatchNumber = Int32.Parse(entMatch.Text);
                await Navigation.PushAsync(HomePage.autoScout, true);
            };
        }

        private void DynamicSizing()
        {
            Layout.Spacing = (App.screenHeight * 4) / 100;
            pckName.HeightRequest = (App.screenHeight * 10) / 100;
            pckName.WidthRequest = (App.screenWidth * 70) / 100;
            pckName.FontSize = (App.screenWidth * 8) / 100;
            pckTeam.HeightRequest = (App.screenHeight * 10) / 100;
            pckTeam.WidthRequest = (App.screenWidth * 70) / 100;
            pckTeam.FontSize = (App.screenWidth * 8) / 100;
            pckAlliance.HeightRequest = (App.screenHeight * 10) / 100;
            pckAlliance.WidthRequest = (App.screenWidth * 70) / 100;
            pckAlliance.FontSize = (App.screenWidth * 8) / 100;
            entMatch.HeightRequest = (App.screenHeight * 10) / 100;
            entMatch.WidthRequest = (App.screenWidth * 70) / 100;
            entMatch.FontSize = (App.screenWidth * 8) / 100;
            btnNextPre.HeightRequest = (App.screenHeight * 10) / 100;
            btnNextPre.WidthRequest = (App.screenWidth * 70) / 100;
            btnNextPre.FontSize = (App.screenWidth * 8) / 100;
        }

        private void InitializeControls()
        {
            foreach (string s in names)
            {
                pckName.Items.Add(s);
            }

            foreach (int i in teams)
            {
                pckTeam.Items.Add(i.ToString());
            }

            pckAlliance.Items.Add("Red");
            pckAlliance.Items.Add("Blue");

            pckName.SelectedIndexChanged += (s, e) =>
            {
                if (pckName.SelectedIndex != -1)
                {
                    hasName = true;
                    enableButton();
                }
            };

            pckTeam.SelectedIndexChanged += (s, e) =>
            {
                if (pckTeam.SelectedIndex != -1)
                {
                    hasTeam = true;
                    enableButton();
                }
            };

            pckAlliance.SelectedIndexChanged += (s, e) =>
            {
                if (pckAlliance.SelectedIndex != -1)
                {
                    hasAlliance = true;
                    enableButton();
                }
            };

            entMatch.TextChanged += (s, e) =>
            {
                if (!entMatch.Text.Equals(""))
                {
                    hasMatch = true;
                    enableButton();
                } else if (entMatch.Text.Equals("")) {
                    hasMatch = false;
                    btnNextPre.IsEnabled = false;
                    btnNextPre.IsVisible = false;
                }
            };

        }

        private void enableButton()
        {
            if (hasName && hasTeam && hasAlliance && hasMatch)
            {
                btnNextPre.IsEnabled = true;
                btnNextPre.IsVisible = true;
            }
        }

    }
}