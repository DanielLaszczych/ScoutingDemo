using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using com.tweirtx.TBAAPIv3client.Api;
using com.tweirtx.TBAAPIv3client.Client;
using com.tweirtx.TBAAPIv3client.Model;
using System.Diagnostics;

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
        private bool hasStation;
        private bool hasAlliance;
        private bool hasMatch;
        List<Match> result;
        EventApi apiInstance;
        internal static Data Data { get; set; }

        public PreScout()
        {
            Data = new Data();
            InitializeComponent();
            hasName = false;
            hasStation = false;
            hasAlliance = false;
            hasMatch = false;
            InitializeControls();
            btnNext.Clicked += async (s, e) => await Navigation.PushAsync(new AutoScout(), true);
        }

        private void InitializeControls()
        {
            Configuration.Default.BasePath = "https://www.thebluealliance.com/api/v3";
            // Configure API key authorization: apiKey
            Configuration.Default.ApiKey.Add("X-TBA-Auth-Key", "musbVwh0cZWMT9y41ZaUTNjVnybkLpZZyyEQkHsD0FIXk0FL4OoFboVVGrKVukXf");
            apiInstance = new EventApi();
            result = apiInstance.GetEventMatches("2019nyny");
            foreach (string s in names)
            {
                pckName.Items.Add(s);
            }

            pckStation.Items.Add("1");
            pckStation.Items.Add("2");
            pckStation.Items.Add("3");

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

            pckAlliance.SelectedIndexChanged += (s, e) =>
            {
                if (pckAlliance.SelectedIndex != -1)
                {
                    Data.SelectedAlliance = pckAlliance.Items[pckAlliance.SelectedIndex];
                    hasAlliance = true;
                    enableButton();
                }
            };

            pckStation.SelectedIndexChanged += (s, e) =>
            {
                if (pckStation.SelectedIndex != -1)
                {
                    Data.SelectedStation = Int32.Parse(pckStation.Items[pckStation.SelectedIndex]);
                    hasStation = true;
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

            btnNext.Clicked +=  (s, e) =>
            {
                var match = result.Find(x => x.MatchNumber == Data.MatchNumber && x.CompLevel == Match.CompLevelEnum.Qm);
                if (Data.SelectedAlliance.Equals("Red"))
                {
                    Data.SelectedTeam = match.Alliances.Red.TeamKeys[Data.SelectedStation - 1].Substring(3);
                }
                else if (Data.SelectedAlliance.Equals("Blue"))
                {
                    Data.SelectedTeam = match.Alliances.Blue.TeamKeys[Data.SelectedStation - 1].Substring(3);
                }
            };
        }

        private void enableButton()
        {
            if (hasName & hasStation & hasAlliance & hasMatch)
            {
                btnNext.IsEnabled = true;
                btnNext.IsVisible = true;
            }
        }

    }
}