using com.tweirtx.TBAAPIv3client.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ScoutingDemo
{
    class Data
    {
        public String SelectedName { get; set; }
        public int SelectedStation { get; set; }
        public static String SelectedTeam { get; set; }
        public String SelectedAlliance { get; set; }
        public int MatchNumber { get; set; }
        public String StartingPiece { get; set; }
        public String StartingPosition { get; set; }
        public String LeftHAB { get; set; }
        public Dictionary<String, String> AutoScored { get; set; }

        public Data()
        {
            AutoScored = new Dictionary<String, String>();
        }

    }
}
