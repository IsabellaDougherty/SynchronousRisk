﻿using static System.Net.Mime.MediaTypeNames;
using System.Runtime.CompilerServices;
using SynchronousRisk.Resources.Assets.Text_Files;

namespace SynchronousRisk
{
    /// Isabella Antonette Dougherty (IAD) 2/5/2025 <summary>
    /// Class to read in .txt file to generate all territories for a board along with all 
    /// required information for each territory.
    /// </summary>
    public class Territories
    {
        /// IAD 2/10/2025 <summary>
        /// Creates all territories via a .txt file named Territories.txt. Note that if you would like to make new .txt files to use this and the subsequent classes that are associated with this class, the formatting will need to be adhered to in the file or the parsing and subsequent functions from it will need to be adjusted.
        /// </summary>
        Territory[] territories;
        private int totalTerritories;
        private int[] territoryRegionIDs;
        private string[] territoryNames;
        public int[] diffRegions;
        private string[][] allBorders;
        /// IAD 2/10/2025 <summary>
        /// Method to read in the territory information from the .txt file that is also a resource.
        /// </summary>
        public Territories(InformationDatasets information)
        {


            //ParseTerritories(ReadInTerritoryInformation());
            //GetSetTotalTerritories();
            //CreateTerritories(territoryNames, territoryRegionIDs, allBorders);
            //for (int i = 0; i < totalTerritories; i++) { Console.WriteLine(territories[i].GetTerritoryInformation()); }
        }
        //IAD 3/6/2025: Method to read in the territory information from the .txt file that is also a resource.
        //public static string ReadInTerritoryInformation() { return Properties.Resources.TerritoriesInformation; }

        ////IAD 2/5/2025: Method to parse the territory information from the .txt file
        //private string[] ParseTerritories(string territoriesString)
        //{
        //    territoriesString = territoriesString.Trim();
        //    string[] territoryInfo = territoriesString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
        //    int territoryCount = territoryInfo.Length;

        //    string[] names = new string[territoryCount];
        //    int[] regionIDs = new int[territoryCount];
        //    string[][] borders = new string[territoryCount][];

        //    for (int i = 0; i < territoryCount; i++)
        //    {
        //        //IAD 2/10/2025: Troubleshooting loop for printing overall territory information
        //        //Console.WriteLine("Territory " + i + ": " + territoryInfo[i]);

        //        string[] territory = territoryInfo[i].Split('\t');

        //        // You might want to check that territory has enough parts.
        //        if (territory.Length < 3)
        //        {
        //            throw new Exception($"Invalid data on line {i + 1}: {territoryInfo[i]}");
        //        }

        //        names[i] = territory[0];
        //        regionIDs[i] = int.Parse(territory[1]);
        //        borders[i] = territory[2].Split(',');
        //    }

        //    //IAD 2/10/2025: Troubleshooting loop for printing individual territory information one line at a time in order it appears in the .txt file
        //    //foreach (string name in names) { Console.WriteLine(name); }
        //    //foreach (int regionid in regionIDs) { Console.WriteLine(regionid); }
        //    //for (int i = 0; i < borders.Length; i++) { foreach (string border in borders[i]) { Console.WriteLine(border); } }

        //    territoryRegionIDs = regionIDs;
        //    SetTerritoryNames(names);
        //    GetSetTotalTerritories();
        //    SetDiffRegions(regionIDs);
        //    SetAllBorders(borders);
        //    return territoryInfo;
        //}


        //IAD 3/17/2025
        /// <summary>
        /// Creates all territories given the names, region IDs, and borders of each territory
        ///     and returns the array of territories created.
        /// </summary>
        /// <param name="names"></param>
        /// <param name="regionIDs"></param>
        /// <param name="borders"></param> 
        public Territory[] CreateTerritories(string[] names, int[][] regionIDs, string[][] borders)
        {
            territories = new Territory[totalTerritories];
            int[] territoryID = new int[3];
            for (int i = 0; i < totalTerritories; i++)
            {
                Troops noTroops = new Troops(-1);
                territories[i] = new Territory(names[i], territoryID, borders[i], noTroops);
            }
            //IAD 2/10/2025: Ensure all territories have borders that are existing territory names
            for (int i = 0; i < totalTerritories; i++)
                territories[i].ExamineBorders(territoryNames);
            return territories;
        }
        /// IAD 2/10/2025 <summary>
        /// Getter methods for Territories class
        /// </summary>
        /// <returns></returns>
        public string[] GetTerritoryNames() { return territoryNames; }
        /// IAD 2/10/2025 <summary>
        /// Returns the different region IDs of the territories
        /// </summary>
        /// <returns></returns>
        public int[] GetDiffRegions() { return diffRegions; }
        /// IAD 2/10/2025 <summary>
        /// Returns the borders of all territories
        /// </summary>
        /// <returns></returns>
        public string[][] GetAllBorders() { return allBorders; }
        /// IAD 2/10/2025 <summary>
        /// Returns total number of territories counting starting at 1 (i.e. if the the territories are 'a' 'b' and 'c' it will return 3)
        /// </summary>
        /// <returns></returns>
        public int GetSetTotalTerritories()
        {
            totalTerritories = territoryNames.Length;
            return totalTerritories;
        }
        /// IAD 2/10/2025 <summary>
        /// Returns the territories array
        /// </summary>
        /// <returns></returns>
        public Territory[] GetTerritories() { return territories; }
        //private void SetTerritoryNames(string[] names) { territoryNames = names; }
        //private void SetDiffRegions(int[] regionIDs)
        //{
        //    List<int> regions = new List<int>();
        //    for (int i = 0; i < regionIDs.Length; i++)
        //    {
        //        if (!(regions.Contains(regionIDs[i]))) regions.Add(regionIDs[i]);
        //    }
        //    diffRegions = regions.ToArray();
        //}
        //IAD 2/10/2025: Set allBorders array to be the same as the borders array
        //private void SetAllBorders(string[][] borders)
        //{
        //    allBorders = new string[totalTerritories][];
        //    for (int i = 0; i < totalTerritories; i++)
        //    {
        //        allBorders[i] = borders[i];
        //    }
        //}
    }
}
