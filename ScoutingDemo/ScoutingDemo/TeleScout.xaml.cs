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
    public partial class TeleScout : ContentPage
    {

        private Submit submit = null;

        public TeleScout()
        {
            InitializeComponent();
            InitializeControl();
            DynamicSizing();
        }

        private void DynamicSizing()
        {
            Layout.Spacing = (App.screenHeight * 2) / 100;
            hatchCShipLabel.FontSize = (App.screenWidth * 4) / 100;
            hatchCShip.Scale = (App.screenHeight * 2.5) / 1000;
            cargoCShipLabel.FontSize = (App.screenWidth * 4) / 100;
            cargoCShip.Scale = (App.screenHeight * 2.5) / 1000;
            hatchRShipLabel.FontSize = (App.screenWidth * 4) / 100;
            hatchRShip.Scale = (App.screenHeight * 2.5) / 1000;
            cargoRShipLabel.FontSize = (App.screenWidth * 4) / 100;
            cargoRShip.Scale = (App.screenHeight * 2.5) / 1000;
            rocketSoloLabel.FontSize = (App.screenWidth * 4) / 100;
            btnYesSolo.FontSize = (App.screenWidth * 5.5) / 100;
            btnYesSolo.HeightRequest = (App.screenHeight * 9) / 100;
            btnYesSolo.WidthRequest = (App.screenHeight * 18) / 100;
            btnNoSolo.FontSize = (App.screenWidth * 5.5) / 100;
            btnNoSolo.HeightRequest = (App.screenHeight * 9) / 100;
            btnNoSolo.WidthRequest = (App.screenHeight * 18) / 100;
            btnNextTele.HeightRequest = (App.screenHeight * 10) / 100;
            btnNextTele.WidthRequest = (App.screenWidth * 25) / 100;
            btnNextTele.FontSize = (App.screenWidth * 5) / 100;
        }

        private void InitializeControl()
        {

            btnNextTele.Clicked += async(s, e) =>
            {
                HomePage.data.HatchPanelCShip = (int)hatchCShip.Value;
                HomePage.data.CargoCShip = (int)cargoCShip.Value;
                HomePage.data.HatchPanelRShip = (int)hatchRShip.Value;
                HomePage.data.CargoRShip = (int)cargoRShip.Value;
                if (submit == null)
                {
                    submit = new Submit();
                }
                await Navigation.PushAsync(submit, true);
            };

            hatchCShip.ValueChanged += (s, e) =>
            {
                hatchCShipLabel.Text = "Hatch Panel Placed (Cargoship): " + hatchCShip.Value;
            };

            cargoCShip.ValueChanged += (s, e) =>
            {
                cargoCShipLabel.Text = "Cargo Placed (Cargoship): " + cargoCShip.Value;
            };

            hatchRShip.ValueChanged += (s, e) =>
            {
                hatchRShipLabel.Text = "Hatch Panel Placed (Left and Right Rocketship): " + hatchRShip.Value;
            };

            cargoRShip.ValueChanged += (s, e) =>
            {
                cargoRShipLabel.Text = "Cargo Placed (Left and Right Rocketship): " + cargoRShip.Value;
            };

            btnYesSolo.Clicked += (s, e) =>
            {
                btnYesSolo.BorderColor = Color.Black;
                btnNoSolo.BorderColor = Color.Transparent;
                btnYesSolo.Opacity = 1;
                btnNoSolo.Opacity = 0.5;
                HomePage.data.SoloedRocket = "Yes";
                btnNextTele.IsEnabled = true;
                btnNextTele.IsVisible = true;
            };

            btnNoSolo.Clicked += (s, e) =>
            {
                btnNoSolo.BorderColor = Color.Black;
                btnYesSolo.BorderColor = Color.Transparent;
                btnNoSolo.Opacity = 1;
                btnYesSolo.Opacity = 0.5;
                HomePage.data.SoloedRocket = "No";
                btnNextTele.IsEnabled = true;
                btnNextTele.IsVisible = true;
            };
        }
    }
}