using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutingDemo
{
    public class Data
    {
        public string SelectedName { get; set; }
        public int SelectedTeam { get; set; }
        public string SelectedAlliance { get; set; }
        public int MatchNumber { get; set; }
        public string StartingPiece { get; set; }
        public string StartingPosition { get; set; }
        public string LeftHAB { get; set; }
        public List<string> gamePiecesAuto { get; set; }
        public List<string> gamePlacesAuto { get; set; }
        public string SoloedRocket { get; set;}
        public int HatchPanelCShip { get; set; }
        public int CargoCShip { get; set; }
        public int HatchPanelRShip { get; set; }
        public int CargoRShip { get; set; }

        public Data()
        {

        }

    }
}
