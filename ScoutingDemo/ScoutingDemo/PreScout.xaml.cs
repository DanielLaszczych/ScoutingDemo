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

        internal static Data Data { get; set; }

        public PreScout()
        {
            Data = new Data();
            InitializeComponent();
            hasName = false;
            hasTeam = false;
            hasAlliance = false;
            hasMatch = false;
            InitializeControls();
            btnNext.Clicked += async (s, e) => await Navigation.PushAsync(new AutoScout(), true);
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
                    Data.SelectedName = pckName.Items[pckName.SelectedIndex];
                    hasName = true;
                    enableButton();
                }
            };

            pckTeam.SelectedIndexChanged += (s, e) =>
            {
                if (pckTeam.SelectedIndex != -1)
                {
                    Data.SelectedTeam = Int32.Parse(pckTeam.Items[pckTeam.SelectedIndex]);
                    hasTeam = true;
                    enableButton();
                }
            };

            pckAlliance.SelectedIndexChanged += (s, e) =>
            {
                if (pckAlliance.SelectedIndex != -1)
                {
                    Data.SelectedAlliance = pckAlliance.Items[pckAlliance.SelectedIndex];
                    hasAlliance = true;
                    enableButton();
                }
            };

            entMatch.TextChanged += (s, e) =>
            {
                if (!entMatch.Text.Equals(""))
                {
                    Data.MatchNumber = Int32.Parse(entMatch.Text);
                    hasMatch = true;
                    enableButton();
                } else if (entMatch.Text.Equals("")) {
                    hasMatch = false;
                    btnNext.IsEnabled = false;
                    btnNext.IsVisible = false;
                }
            };

        }

        private void enableButton()
        {
            if (hasName & hasTeam & hasAlliance & hasMatch)
            {
                btnNext.IsEnabled = true;
                btnNext.IsVisible = true;
            }
        }

    }
}