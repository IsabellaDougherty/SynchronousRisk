using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    public class Territory
    {
        public string n;
        public int r;
        public string[] b;
        //public Player pID;
        public Territory(string names, int regionID, string[] borders /*, Player playerID,*/)
        {
            n = names;
            r = regionID;
            b = borders;
            //p = playerID;
        }
        //IAD 2/10/2025: Ensure all territories have borders that are existing territory names
        public void ExamineBorders(string[] allTerritoryNames) 
        { for(int i = 0; i < b.Length; i++)
            { if (!allTerritoryNames.Contains(b[i])) {
                    throw new Exception("\nError: Territory " + b[i] + " does not exist."); } }
        }


        //IAD 2/10/2025: Getter methods for Territory class
        public string GetName() { return n; }
        public int GetRegionID() { return r; }
        public string[] GetBorders() { return b; }

        //public Player GetPlayerID() { return pID; }

        //IAD 2/10/2025: Returns the territory's name, region ID, and borders (Made for troubleshooting/testing)
        public string GetTerritoryInformation()
        {
            return "Territory Name: " + GetName() + "\nRegion ID: " + GetRegionID() + "\nBorders: " + GetBorders();
        }
    }
}
