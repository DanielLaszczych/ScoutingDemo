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
    public partial class AutoScout : ContentPage
    {

        private TeleScout teleScout = null;

        private readonly List<String> positions = new List<string>
        {
            "Level 1- Right", "Level 1- Left", "Level 1- Center",
            "Level 2- Right", "Level 2- Left"
        };

        private readonly List<String> gamePieces = new List<string>
        {
            "Cargo", "Hatch Panel", "Remove Row"
        };

        private readonly List<String> scoringPositions = new List<string>
        {
            "Cargoship Left", "Cargoship Front", "Cargoship Right",
            "Rocketship Nearside", "Rocketship Farside", "None"
        };

        private List<String> scoredPieces = new List<String>();
        private List<String> scoredPlaces = new List<String>();

        private bool hasPosition;
        private bool hasPiece;
        private bool hasHAB;
        private bool hasScoredPosition;
        private bool hasAdditional;

        private int count = 3;
        private int rowNumber = 1;

        public AutoScout()
        {
            InitializeComponent();
            InitializeControls();
            DynamicSizing();
            hasPosition = false;
            hasPiece = false;
            hasHAB = false;
            hasScoredPosition = false;
            hasAdditional = false;
        }

        private void DynamicSizing()
        {
            Layout.Spacing = (App.screenHeight * 1) / 100;
            pckPosition.HeightRequest = (App.screenHeight * 9) / 100;
            pckPosition.WidthRequest = (App.screenWidth * 56.5) / 100;
            pckPosition.FontSize = (App.screenWidth * 7.5) / 100;
            firstLabel.FontSize = (App.screenWidth * 5) / 100;
            btnHatch.FontSize = (App.screenWidth * 5) / 100;
            btnHatch.HeightRequest = (App.screenHeight * 10) / 100;
            btnHatch.WidthRequest = (App.screenHeight * 16) / 100;
            btnCargo.FontSize = (App.screenWidth * 5) / 100;
            btnCargo.HeightRequest = (App.screenHeight * 10) / 100;
            btnCargo.WidthRequest = (App.screenHeight * 16) / 100;
            btnNone.FontSize = (App.screenWidth * 5) / 100;
            btnNone.HeightRequest = (App.screenHeight * 10) / 100;
            btnNone.WidthRequest = (App.screenHeight * 16) / 100;
            secondLabel.FontSize = (App.screenWidth * 5) / 100;
            btnYesHAB.FontSize = (App.screenWidth * 5.5) / 100;
            btnYesHAB.HeightRequest = (App.screenHeight * 9) / 100;
            btnYesHAB.WidthRequest = (App.screenHeight * 18) / 100;
            btnNoHAB.FontSize = (App.screenWidth * 5.5) / 100;
            btnNoHAB.HeightRequest = (App.screenHeight * 9) / 100;
            btnNoHAB.WidthRequest = (App.screenHeight * 18) / 100;
            thirdLabel.FontSize = (App.screenWidth * 5) / 100;
            btnCargoShip.FontSize = (App.screenWidth * 5) / 100;
            btnCargoShip.HeightRequest = (App.screenHeight * 12) / 100;
            btnCargoShip.WidthRequest = (App.screenHeight * 18.5) / 100;
            btnRocketShip.FontSize = (App.screenWidth * 5) / 100;
            btnRocketShip.HeightRequest = (App.screenHeight * 12) / 100;
            btnRocketShip.WidthRequest = (App.screenHeight * 18.5) / 100;
            btnNoneShip.FontSize = (App.screenWidth * 5) / 100;
            btnNoneShip.HeightRequest = (App.screenHeight * 9) / 100;
            btnNoneShip.WidthRequest = (App.screenHeight * 18.5) / 100; //Got here
            pckConfirmSecond.HeightRequest = (App.screenHeight * 7) / 100;
            pckConfirmSecond.WidthRequest = (App.screenWidth * 37) / 100;
            pckConfirmSecond.FontSize = (App.screenWidth * 5) / 100;
            pckSecondShip.HeightRequest = (App.screenHeight * 7) / 100;
            pckSecondShip.WidthRequest = (App.screenWidth * 37) / 100;
            pckSecondShip.FontSize = (App.screenWidth * 4.5) / 100;
            btnNextAuto.HeightRequest = (App.screenHeight * 10) / 100;
            btnNextAuto.WidthRequest = (App.screenWidth * 25) / 100;
            btnNextAuto.FontSize = (App.screenWidth * 5) / 100;
        }

        private void InitializeControls()
        {

            btnNextAuto.Clicked += async (s, e) =>
            {
                if (teleScout == null)
                {
                    teleScout = new TeleScout();
                }
                if (scoredPieces.Count == scoredPlaces.Count)
                {
                    HomePage.data.gamePiecesAuto = scoredPieces;
                    HomePage.data.gamePlacesAuto = scoredPlaces;  
                }
                await Navigation.PushAsync(teleScout, true);
            };

            foreach (String s in positions)
            {
                pckPosition.Items.Add(s);
            }

            foreach (String s in gamePieces)
            {
                pckConfirmSecond.Items.Add(s);
            }
            pckConfirmSecond.Items.Remove("Remove Row");
            pckConfirmSecond.Items.Add("Finished");

            foreach (String s in scoringPositions)
            {
                pckSecondShip.Items.Add(s);
            }


            pckPosition.SelectedIndexChanged += (s, e) => {
                HomePage.data.StartingPosition = pckPosition.Items[pckPosition.SelectedIndex];
                hasPosition = true;
                checkEndButton();
            };

            btnCargo.Clicked += (s, e) =>
            {
                btnCargo.Opacity = 1;
                btnHatch.Opacity = 0.5;
                btnNone.Opacity = 0.5;
                btnCargo.BorderColor = Color.Black;
                btnHatch.BorderColor = Color.Transparent;
                btnNone.BorderColor = Color.Transparent;
                hasPiece = true;
                HomePage.data.StartingPiece = "Cargo";
                if (scoredPieces.Count > 0)
                {
                    scoredPieces.RemoveAt(0);
                }
                scoredPieces.Insert(0, "Cargo");
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            btnHatch.Clicked += (s, e) =>
            {
                btnHatch.Opacity = 1;
                btnCargo.Opacity = 0.5;
                btnNone.Opacity = 0.5;
                btnCargo.BorderColor = Color.Transparent;
                btnHatch.BorderColor = Color.Black;
                btnNone.BorderColor = Color.Transparent;
                hasPiece = true;
                HomePage.data.StartingPiece = "Hatch Panel";
                if (scoredPieces.Count > 0)
                {
                    scoredPieces.RemoveAt(0);
                }
                scoredPieces.Insert(0, "Hatch Panel");
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            btnNone.Clicked += (s, e) =>
            {
                btnNone.Opacity = 1;
                btnCargo.Opacity = 0.5;
                btnHatch.Opacity = 0.5;
                btnCargo.BorderColor = Color.Transparent;
                btnHatch.BorderColor = Color.Transparent;
                btnNone.BorderColor = Color.Black;
                hasPiece = true;
                HomePage.data.StartingPiece = "None";
                if (scoredPieces.Count > 0)
                {
                    scoredPieces.RemoveAt(0);
                }
                scoredPieces.Insert(0, "None");
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            btnYesHAB.Clicked += (s, e) =>
            {
                btnYesHAB.Opacity = 1;
                btnNoHAB.Opacity = 0.5;
                btnYesHAB.BorderColor = Color.Black;
                btnNoHAB.BorderColor = Color.Transparent;
                hasHAB = true;
                HomePage.data.LeftHAB = "Yes";
                checkEndButton();
            };

            btnNoHAB.Clicked += (s, e) =>
            {
                btnNoHAB.Opacity = 1;
                btnYesHAB.Opacity = 0.5;
                btnYesHAB.BorderColor = Color.Transparent;
                btnNoHAB.BorderColor = Color.Black;
                hasHAB = true;
                HomePage.data.LeftHAB = "No";
                checkEndButton();
            };

            btnCargoShip.Clicked += async (s, e) =>
            {
                btnCargoShip.Opacity = 1;
                btnRocketShip.Opacity = 0.5;
                btnNoneShip.Opacity = 0.5;
                btnCargoShip.BorderColor = Color.Black;
                btnRocketShip.BorderColor = Color.Transparent;
                btnNoneShip.BorderColor = Color.Transparent;
                if (scoredPlaces.Count > 0)
                {
                    scoredPlaces.RemoveAt(0);
                }
                String specificLocation = null;
                while (specificLocation == null)
                {
                    specificLocation = await DisplayActionSheet("Which side was the piece scored?", null, null, "Left", "Front", "Right");
                }
                scoredPlaces.Insert(0, "Cargoship " + specificLocation);
                hasScoredPosition = true;
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            btnRocketShip.Clicked += async (s, e) =>
            {
                btnRocketShip.Opacity = 1;
                btnCargoShip.Opacity = 0.5;
                btnNoneShip.Opacity = 0.5;
                btnCargoShip.BorderColor = Color.Transparent;
                btnRocketShip.BorderColor = Color.Black;
                btnNoneShip.BorderColor = Color.Transparent;
                if (scoredPlaces.Count > 0)
                {
                    scoredPlaces.RemoveAt(0);
                }
                String specificLocation = null;
                while (specificLocation == null)
                {
                    specificLocation = await DisplayActionSheet("Which side was the piece scored?", null, null, "Nearside", "Farside");
                }
                scoredPlaces.Insert(0, "Rocketship " + specificLocation);
                hasScoredPosition = true;
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            btnNoneShip.Clicked += (s, e) =>
            {
                btnNoneShip.Opacity = 1;
                btnCargoShip.Opacity = 0.5;
                btnRocketShip.Opacity = 0.5;
                btnCargoShip.BorderColor = Color.Transparent;
                btnRocketShip.BorderColor = Color.Transparent;
                btnNoneShip.BorderColor = Color.Black;
                if (scoredPlaces.Count > 0)
                {
                    scoredPlaces.RemoveAt(0);
                }
                scoredPlaces.Insert(0, "None");
                hasScoredPosition = true;
                if (hasFirstPieceDone())
                {
                    pckConfirmSecond.IsVisible = true;
                    pckConfirmSecond.IsEnabled = true;
                }
                checkEndButton();
            };

            pckConfirmSecond.SelectedIndexChanged += (s, e) =>
            {
                if (pckConfirmSecond.Items.Contains("Finished"))
                {
                    if (pckConfirmSecond.Items[pckConfirmSecond.SelectedIndex].Equals("Finished"))
                    {
                        hasAdditional = true;
                        if (scoredPieces.Count > 1)
                        {
                            scoredPieces.RemoveAt(1);
                        }
                        if (scoredPlaces.Count > 1)
                        {
                            scoredPlaces.RemoveAt(1);
                        }
                        checkEndButton();
                    }
                    else
                    {
                        hasAdditional = false;
                        checkEndButton();
                    }
                }
                if (!pckConfirmSecond.Items[pckConfirmSecond.SelectedIndex].Equals("Finished"))
                {
                    pckSecondShip.IsVisible = true;
                    pckSecondShip.IsEnabled = true;
                    if (scoredPieces.Count > 1)
                    {
                        scoredPieces.RemoveAt(1);
                    }
                    scoredPieces.Insert(1, pckConfirmSecond.Items[pckConfirmSecond.SelectedIndex]);
                } 
                else 
                {
                    pckSecondShip.IsVisible = false;
                    pckSecondShip.IsEnabled = false;
                    pckSecondShip.SelectedIndex = -1;
                    if (scoredPieces.Count > 1)
                    {
                        scoredPieces.RemoveAt(1);
                    }
                    if (scoredPlaces.Count > 1)
                    {
                        scoredPlaces.RemoveAt(1);
                    }
                }
            };

            pckSecondShip.SelectedIndexChanged += (s, e) =>
            {
                if (pckSecondShip.SelectedIndex != -1) { 
                    if (scoredPlaces.Count > 1)
                    {
                        scoredPlaces.RemoveAt(1);
                    }
                    scoredPlaces.Insert(1, pckSecondShip.Items[pckSecondShip.SelectedIndex]);
                }
            };

            pckSecondShip.Unfocused += (s, e) =>
            {
                if (pckSecondShip.SelectedIndex != -1)
                {
                    if (count == 3)
                    {
                        addRow();
                    }
                }
            };
        }

        private bool hasFirstPieceDone()
        {
            return hasPiece && hasScoredPosition;
        }

        private void addRow()
        {
            RowDefinition row = new RowDefinition { Height = GridLength.Auto };
            LastGrid.RowDefinitions.Add(row);
            string addon = (count > 3) ? "th Piece" : "rd Piece";
            Picker p1 = new Picker {
                Title = count + addon,
                HeightRequest = (App.screenHeight * 7) / 100,
                WidthRequest = (App.screenWidth * 37) / 100,
                FontSize = (App.screenWidth * 5) / 100
            };
            count++;
            foreach (String s in gamePieces)
            {
                p1.Items.Add(s);
            }
            Picker priorPicker = (Picker)LastGrid.Children[(rowNumber - 1) * 2];
            if (priorPicker.Items.Contains("Finished"))
            {
                priorPicker.Items.Remove("Finished");
                p1.Items.Add("Finished");
            }
            Picker p2 = new Picker
            {
                Title = "Scored where",
                HeightRequest = (App.screenHeight * 7) / 100,
                WidthRequest = (App.screenWidth * 37) / 100,
                FontSize = (App.screenWidth * 4.5) / 100,
                IsEnabled = false,
                IsVisible = false
            };
            foreach (String s in scoringPositions)
            {
                p2.Items.Add(s);
            }
            LastGrid.Children.Add(p1, 0, rowNumber);
            LastGrid.Children.Add(p2, 1, rowNumber++);
            p1.SelectedIndexChanged += (s, e) =>
            {
                if (p1.Items.Contains("Finished"))
                {
                    if (p1.Items[p1.SelectedIndex].Equals("Finished"))
                    {
                        hasAdditional = true;
                        p2.IsEnabled = false;
                        p2.IsVisible = false;
                        p2.SelectedIndex = -1;
                        int rowToCheck = Grid.GetRow(p1) + 1;
                        if (scoredPlaces.Count > rowToCheck && scoredPieces.Count > rowToCheck)
                        {
                            scoredPlaces.RemoveAt(rowToCheck);
                            scoredPieces.RemoveAt(rowToCheck);
                        }
                        checkEndButton();
                    }
                    else
                    {
                        hasAdditional = false;
                        checkEndButton();
                    }
                }
                if (!p1.Items[p1.SelectedIndex].Equals("Remove Row") && !p1.Items[p1.SelectedIndex].Equals("Finished"))
                {
                    if (scoredPieces.Count > (Grid.GetRow(p1) + 1))
                    {
                        scoredPieces.RemoveAt((Grid.GetRow(p1) + 1));
                    }
                    scoredPieces.Insert((Grid.GetRow(p1) + 1), p1.Items[p1.SelectedIndex]);
                    p2.IsVisible = true;
                    p2.IsEnabled = true;
                }
                else if (p1.Items[p1.SelectedIndex].Equals("Remove Row"))
                {
                    var picker = (Picker)s;
                    var rowToBeRemoved = Grid.GetRow(picker);
                    var children = LastGrid.Children.ToList();
                    LastGrid.RowDefinitions.RemoveAt(rowToBeRemoved);
                    if (picker.Items.Contains("Finished"))
                    {
                        Picker priorPicker1 = (Picker)LastGrid.Children[(rowToBeRemoved - 1) * 2];
                        priorPicker1.Items.Add("Finished");
                    }
                    foreach (var child in children.Where(child => Grid.GetRow(child) == rowToBeRemoved))
                    {
                        LastGrid.Children.Remove(child);
                    }
                    foreach (var child in children.Where(child => Grid.GetRow(child) > rowToBeRemoved))
                    {
                        var modifiedChild = (Picker)child;
                        String oldTitle = modifiedChild.Title;
                        String newTitle;
                        bool canCovert = Int32.TryParse(oldTitle.Substring(0, 1), out int newNumber);
                        if (canCovert)
                        {
                            addon = ((newNumber - 1) > 3) ? "th Piece" : "rd Piece";
                            newTitle = (newNumber - 1) + addon;
                            modifiedChild.Title = newTitle;
                        }
                        Grid.SetRow(modifiedChild, Grid.GetRow(child) - 1);
                    }
                    var rowToCheck = rowToBeRemoved + 1;
                    if (scoredPlaces.Count > rowToCheck && scoredPieces.Count > rowToCheck)
                    {
                        scoredPlaces.RemoveAt(rowToCheck);
                        scoredPieces.RemoveAt(rowToCheck);
                    }
                    count--;
                    rowNumber--;
                }
            };

            p2.SelectedIndexChanged += (s, e) =>
            {
                if (p2.SelectedIndex != -1)
                {
                    if (scoredPlaces.Count > (Grid.GetRow(p2) + 1))
                    {
                        scoredPlaces.RemoveAt((Grid.GetRow(p1) + 1));
                    }
                    scoredPlaces.Insert((Grid.GetRow(p1) + 1), p2.Items[p2.SelectedIndex]);
                    printList();
                }
            };

            p2.Unfocused += (s, e) =>
            {
                if (p2.SelectedIndex != -1)
                {
                    if ((Grid.GetRow(p2) + 1) == LastGrid.RowDefinitions.Count)
                    {
                        addRow();
                    }
                }
            };
        }

        private void printList()
        {
            foreach (String s in scoredPieces)
            {
                Console.WriteLine(s);
            }
            foreach (String s in scoredPlaces)
            {
                Console.WriteLine(s);
            }
        }

        private void checkEndButton()
        {
            if (hasPosition && hasPiece && hasHAB && hasScoredPosition && hasAdditional)
            {
                btnNextAuto.IsVisible = true;
                btnNextAuto.IsEnabled = true;
            } else
            {
                btnNextAuto.IsVisible = false;
                btnNextAuto.IsEnabled = false;
            }
        }
    }
}