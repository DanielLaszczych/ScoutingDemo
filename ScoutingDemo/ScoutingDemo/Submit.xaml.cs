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
    public partial class Submit : ContentPage
    {
        public Submit()
        {
            InitializeComponent();
            btnSubmit.HeightRequest = (App.screenHeight * 22) / 100;
            btnSubmit.WidthRequest = (App.screenWidth * 60) / 100;
            btnSubmit.FontSize = (App.screenWidth * 13) / 100;
            btnSubmit.CornerRadius = 10;
            btnSubmit.Clicked += async(s, e) =>
            {
                GoogleFormsSubmissionService form = new GoogleFormsSubmissionService("https://docs.google.com/forms/d/e/1FAIpQLSeZBT1kXj8_94F_jv6ndowaJ1nIPB0iQjZ_sOatrDJ8boXhpA/formResponse");
                var fields = new Dictionary<string, string>
                {
                    { "entry.1235181715", HomePage.data.SelectedName },
                    { "entry.688283936", HomePage.data.SelectedTeam.ToString()},
                    { "entry.801511334", HomePage.data.SelectedAlliance},
                    { "entry.134261437", HomePage.data.MatchNumber.ToString()},
                    { "entry.523403633", HomePage.data.StartingPosition},
                    { "entry.1677740116", HomePage.data.gamePiecesAuto[0]},
                    { "entry.1206590888", HomePage.data.LeftHAB},
                    { "entry.1264935609", HomePage.data.gamePlacesAuto[0]},
                    { "entry.1534945902", HomePage.data.gamePiecesAuto.Count > 1 ? HomePage.data.gamePiecesAuto[1] : "None"},
                    { "entry.1042147908", HomePage.data.gamePlacesAuto.Count > 1 ? HomePage.data.gamePlacesAuto[1] : "None"},
                    { "entry.7775528", HomePage.data.HatchPanelCShip.ToString()},
                    { "entry.1236637632", HomePage.data.CargoCShip.ToString()},
                    { "entry.2087994661", HomePage.data.HatchPanelCShip.ToString()},
                    { "entry.535075833", HomePage.data.CargoRShip.ToString()},
                    { "entry.1717391970", HomePage.data.SoloedRocket}
                };
                form.SetFieldValues(fields);
                var response = await form.SubmitAsync();
                await Navigation.PopToRootAsync();
            };
        }
    }
}