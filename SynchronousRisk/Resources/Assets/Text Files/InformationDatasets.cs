using System;
using System.Collections.Generic;
using System.Drawing;

namespace SynchronousRisk.Resources.Assets.Text_Files
{
    /// IAD 3/10/2025 <summary> Parses input strings as needed. </summary>
    public class InformationDatasets
    {
        /// IAD 3/16/2025 <summary> Fields for the InformationDatasets class to be accessible through other classes. </summary>
        public Dictionary<string, Territory> territoryLookup = new Dictionary<string, Territory>();
        public Dictionary<string, List<Territory>> borders = new Dictionary<string, List<Territory>>();
        public Dictionary<int, List<Territory>> regions = new Dictionary<int, List<Territory>>();
        public Dictionary<int[], Territory> rgbLookup = new Dictionary<int[], Territory>();
        public Bitmap[] playerIcons;
        /// 3/10/2025 <summary> Constructor for the InformationDatasets class. </summary>
        public InformationDatasets()
        {
            TerritoryInformationParse();
            playerIcons = FileReadIn.PlayerIcons();
        }
        /// IAD 3/16/2025 <summary> Parses out information read in from the FileReadIn class regarding territories. </summary>
        /// <exception cref="Exception"></exception>
        private void TerritoryInformationParse()
        {
            string[] lineSplit = FileReadIn.TerritoryInformation().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] names = new string[lineSplit.Length];
            string[] territoryRGBStrings = new string[lineSplit.Length];
            string[][] borders = new string[lineSplit.Length][];
            string[] stringPositions = new string[lineSplit.Length];
            int index = 0;
            foreach (string line in lineSplit)
            {
                string[] tabSplit = line.Split('\t');
                if (tabSplit.Length < 4) throw new Exception($"Invalid data on line {index + 1}: {line[index]}");
                names[index] = tabSplit[0].Trim();
                territoryRGBStrings[index] = tabSplit[1].Trim();
                borders[index] = tabSplit[2].Split(',');
                stringPositions[index] = tabSplit[3].Trim();
                foreach (string border in borders[index]) border.Trim();
                index++;
            }
            index = 0;
            int[][] territoryRGBs = new int[territoryRGBStrings.Length][];
            foreach (string RGB in territoryRGBStrings) { territoryRGBs[index] = RGBParse(RGB); index++; }
            List<PointF> positionsList = new List<PointF>();
            foreach (string pos in stringPositions) 
                positionsList.Add(PositionParse(pos));
            PointF[] positions = new PointF[positionsList.Count];
            positions = positionsList.ToArray();
            FillTerritoryLookup(names, territoryRGBs, positions, borders);
        }
        /// IAD 3/17/2025 <summary> Parses input RGB string into a size 3 int array with int[0] being red, int[1] being green, and int[2] being blue. </summary>
        /// <param name="rgb"></param> <exception cref="Exception"></exception>
        private int[] RGBParse(string rgb)
        {
            string[] RGBSplit = rgb.Split(',');
            if (RGBSplit.Length < 3) throw new Exception($"Invalid number of RGB values.");
            int[] output = new int[3];
            if (int.TryParse(RGBSplit[0], out int r) && int.TryParse(RGBSplit[1], out int g) && int.TryParse(RGBSplit[2], out int b))
            { output[0] = int.Parse(RGBSplit[0]); output[1] = int.Parse(RGBSplit[1]); output[2] = int.Parse(RGBSplit[2]); }
            else throw new Exception($"Invalid RGB values.");
            return output;
        }
        /// IAD 3/31/2025 <summary> Parses input position string into a Point object with x and y values. </summary> 
        /// <param name="position"></param> <exception cref="Exception"></exception>
        private PointF PositionParse(string position)
        {
            string[] positionSplit = position.Split(',');
            if (positionSplit.Length < 2) throw new Exception($"Invalid number of position values.");
            else foreach (string pos in positionSplit) pos.Trim();
            PointF output = new PointF();
            if (double.TryParse(positionSplit[0], out double x) && double.TryParse(positionSplit[1], out double y))
            { 
                output.X = (float)double.Parse(positionSplit[0]); 
                output.Y = (float)double.Parse(positionSplit[1]); 
            }
            else throw new Exception($"Invalid position values.");
            return output;
        }
        /// IAD 3/17/2025 <summary> Fills the territory lookup dictionary with information provided from the parameters. </summary>
        /// <param name="names"></param> <param name="territoryIDs"></param> <param name="borders"></param>
        private void FillTerritoryLookup(string[] names, int[][] territoryIDs, PointF[] positions, string[][] borders)
        {
            for (int i = 0; i < names.Length; i++)
            {
                Territory tempTerr = new Territory(names[i], territoryIDs[i], borders[i], positions[i], new Troops(-1));
                string name = names[i];
                territoryLookup.Add(name, tempTerr);
                FillrgbLookup(tempTerr);
                FillRegions(tempTerr);
                FillBorders(tempTerr);
            }
        }
        /// IAD 3/17/2025 <summary> Fills the regions dictionary with the territories provided. </summary>
        /// <param name="territory"></param>
        private void FillRegions(Territory territory) 
        {
            if (!regions.ContainsKey(territory.GetRegionID()))
                regions.Add(territory.GetRegionID(), new List<Territory>());
            else regions[territory.GetRegionID()].Add(territory);
            foreach (int[] rgb in rgbLookup.Keys)if (rgb[0] == territory.GetRegionID()) { regions[territory.GetRegionID()].Add(rgbLookup[rgb]); }
        }
        /// IAD 3/17/2025 <summary> Fills the borders dictionary with the territories provided. </summary>
        /// <param name="territory"></param>
        private void FillBorders(Territory territory)
        {
            string[] borderString = territory.GetBorders();
            List<Territory> borderingTerrs = new List<Territory>();
            foreach (string border in borderString)
            {
                if (territoryLookup.ContainsKey(border)) { borderingTerrs.Add(territoryLookup[border]); }
                else { Console.WriteLine($"Warning: Territory '{border}' (border of '{territory.GetName()}') not found."); }
            }
            borders[territory.GetName()] = borderingTerrs;
        }
        /// IAD 3/16/2025 <summary> Fills the rgbLookup dictionary with the territories provided. </summary>
        /// <param name="territory"></param>
        private void FillrgbLookup(Territory territory) { rgbLookup[territory.GetRGB()] = territory; }
        /// IAD 3/17/2025 <summary> Returns an array of all the territories. </summary>
        public Territory[] GetTerritoriesArray()
        {
            Territory[] output = new Territory[territoryLookup.Keys.Count];
            int index = 0;
            foreach (string name in territoryLookup.Keys) { output[index] = territoryLookup[name]; index++; }
            return output;
        }
        /// IAD 3/17/2025 <summary> Returns an array of all the borders for a provided territory. </summary>
        public Territory[] GetTerritoriesBordersArray(string name) { return borders[name].ToArray(); }
        /// IAD 3/17/2025 <summary> Returns the territory with the provided name given the RGB value. </summary>
        /// <param name="rgb"></param>
        public Territory GetTerritoryByRGB(int[] rgb) { return rgbLookup[rgb]; }
    }
}