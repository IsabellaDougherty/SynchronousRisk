using SynchronousRisk.obj.Game_Pieces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    public class Territory
    {
        /// IAD 2/10/2025 <summary>
        /// Variables for the Territory class.
        /// </summary>
        public string n;
        public int[] rgb;
        public string[] b;
        public Troops t;
        public PointF position;
        public bool iconChange;
        public bool troopChange;

        public Wormhole ExitWormhole;
        /// IAD 2/10/2025 <param name="names"></param>
        /// <param name="RGB"></param>
        /// <param name="borders"></param>
        /// <param name="troopsPresent"></param>
        public Territory(string name, int[] RGB, string[] borders, PointF p, Troops troopsPresent)
        {
            n = name;
            rgb = RGB;
            b = borders;
            position = p;
            iconChange = false;
            troopChange = false;
            if (troopsPresent.getTroops() != 0) t = troopsPresent;
            else t.setTroops(-1);
        }

        /// Russell Phillips 4/21/2025
        /// <summary> 
        /// Creates a dummy territory with a specified number of troops
        /// </summary>
        /// <param name="NumTroops"></param>
        public Territory(int NumTroops)
        {
            t = new Troops(NumTroops);
        }
        /// IAD 2/10/2025 <summary>
        /// Ensure all territories have borders that are existing territory names
        /// </summary>
        /// <param name="allTerritoryNames"></param>
        /// <exception cref="Exception"></exception>
        public void ExamineBorders(string[] allTerritoryNames) 
        { for(int i = 0; i < b.Length; i++)
            { if (!allTerritoryNames.Contains(b[i])) {
                    throw new Exception("\nError: Territory " + b[i] + " does not exist."); } }
        }
        /// IAD 2/10/2025 <summary>
        /// Getter methods for Territory class
        /// </summary>
        /// <returns></returns>
        public string GetName() { return n; }
        public PointF GetPosition() { return position; }
        /// IAD 2/10/2025 <summary>
        /// Returns the region ID of the territory
        /// </summary>
        /// <returns></returns>
        public int GetRegionID() { return rgb[0]; }
        /// IAD 2/10/2025 <summary>
        /// Returns the RGB values of the territory
        /// </summary>
        /// <returns></returns>
        public int[] GetRGB() { return rgb; }
        /// IAD 2/10/2025 <summary>
        /// Returns the borders of the territory
        /// </summary>
        /// <returns></returns>
        public string[] GetBorders() { return b; }
        /// IAD 2/10/2025 <summary>
        /// Returns the territory's name, region ID, and borders (Made for troubleshooting/testing)
        /// </summary>
        /// <returns></returns>
        public string GetTerritoryInformation()
        {
            return "Territory Name: " + GetName() + "\nRegion ID: " + GetRegionID() + "\nBorders: " + GetBorders();
        }
        /// IAD 2/10/2025 <summary>
        /// Returns the number of troops present in the territory
        /// </summary>
        /// <returns></returns>
        public int GetTroops() { return t.getTroops(); }
        /// IAD 2/10/2025 <summary>
        /// Sets the number of troops present in the territory
        /// </summary>
        /// <param name="newTroops"></param>
        public void SetTroops(int newTroops) { 
            t.setTroops(newTroops);
            troopChange = true;
        }
    }
}
