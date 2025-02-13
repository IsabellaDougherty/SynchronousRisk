using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SynchronousRisk
{
    /*Isabella Antonette Dougherty (IAD)
     2/5/2025
    Class to read in .txt file to generate all territories for a board along with all required information for each territory.*/
    public class Territories
    {
        Territory[] territories;
        private int totalTerritories; 
        private int[] territoryRegionIDs;
        private string[] territoryNames;
        private int[] diffRegions;
        private string[][] allBorders;
        public Territories()
        {
            ParseTerritories(ReadInTerritoryInformation());
            GetSetTotalTerritories();
            CreateTerritories(territoryNames, territoryRegionIDs, allBorders);
            for (int i = 0; i < totalTerritories; i++) { Console.WriteLine(territories[i].GetTerritoryInformation()); }
        }

        /*Following code to read in file taken and altered from https://stackoverflow.com/questions/3314140/how-to-read-embedded-resource-text-file */
        public static string ReadInTerritoryInformation()
        {
            var resourceName = "SynchronousRisk.Territories.txt";  // Adjust based on your actual namespace/folder structure

            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    // For debugging: list available resource names
                    var available = string.Join(", ", assembly.GetManifestResourceNames());
                    throw new InvalidOperationException(
                        $"Resource '{resourceName}' not found. Available resources: {available}");
                }
                using (var reader = new StreamReader(stream))
                {
                    string content = reader.ReadToEnd();
                    //Console.WriteLine("Successfully read file to be: " + content + "\nEnd of file.");
                    return content;
                }
            }
        }
        //End of borrowed code

        //IAD 2/5/2025: Method to parse the territory information from the .txt file
        private string[] ParseTerritories(string territoriesString)
        {
            territoriesString = territoriesString.Trim();
            string[] territoryInfo = territoriesString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            int territoryCount = territoryInfo.Length;

            string[] names = new string[territoryCount];
            int[] regionIDs = new int[territoryCount];
            string[][] borders = new string[territoryCount][];

            for (int i = 0; i < territoryCount; i++)
            {
                //IAD 2/10/2025: Troubleshooting loop for printing overall territory information
                //Console.WriteLine("Territory " + i + ": " + territoryInfo[i]);

                string[] territory = territoryInfo[i].Split('\t');

                // You might want to check that territory has enough parts.
                if (territory.Length < 3)
                {
                    throw new Exception($"Invalid data on line {i + 1}: {territoryInfo[i]}");
                }

                names[i] = territory[0];
                regionIDs[i] = int.Parse(territory[1]);
                borders[i] = territory[2].Split(',');
            }

            //IAD 2/10/2025: Troubleshooting loop for printing individual territory information one line at a time in order it appears in the .txt file
            //foreach (string name in names) { Console.WriteLine(name); }
            //foreach (int regionid in regionIDs) { Console.WriteLine(regionid); }
            //for (int i = 0; i < borders.Length; i++) { foreach (string border in borders[i]) { Console.WriteLine(border); } }

            territoryRegionIDs = regionIDs;
            SetTerritoryNames(names);
            GetSetTotalTerritories();
            SetDiffRegions(regionIDs);
            SetAllBorders(borders);
            return territoryInfo;
        }

        private void CreateTerritories(string[] names, int[] regionIDs, string[][] borders)
        {
            territories = new Territory[totalTerritories];
            for (int i = 0; i < totalTerritories; i++)
            {
                territories[i] = new Territory(names[i], regionIDs[i], borders[i]);
            }
            //IAD 2/10/2025: Ensure all territories have borders that are existing territory names
            for (int i = 0; i < totalTerritories; i++)
            {
                territories[i].ExamineBorders(territoryNames);
            }
        }

        public string[] GetTerritoryNames() { return territoryNames; }
        public int[] GetDiffRegions() { return diffRegions; }
        public string[][] GetAllBorders() { return allBorders; }
        //IAD 2/10/2025: Returns total number of territories counting starting at 1 (i.e. if the the territories are 'a' 'b' and 'c' it will return 3)
        public int GetSetTotalTerritories()
        {
            totalTerritories = territoryNames.Length;
            return totalTerritories;
        }
        public Territory[] GetTerritories()
        {
            return territories;
        }
        private void SetTerritoryNames(string[] names) { territoryNames = names; }
        private void SetDiffRegions(int[] regionIDs)
        {
            List<int> regions = new List<int>();
            for (int i = 0; i < regionIDs.Length; i++)
            {
                if (!(regions.Contains(regionIDs[i]))) regions.Add(regionIDs[i]);
            }
            diffRegions = regions.ToArray();
        }
        //IAD 2/10/2025: Set allBorders array to be the same as the borders array
        private void SetAllBorders(string[][] borders) {
            allBorders = new string[totalTerritories][];
            for (int i = 0; i < totalTerritories; i++) { 
                allBorders[i] = borders[i]; }  }
    }
}
