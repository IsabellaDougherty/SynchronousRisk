using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.Resources.Assets.Text_Files
{
    //IAD 3/10/2025
    /// <summary> Parses input strings as needed. </summary> <returns></returns>
    public class InformationDatasets
    {
        //FileReadIn files = new FileReadIn();

        Dictionary<string, Territory> territoryLookup;
        Dictionary<string, List<Territory>> borders;
        Dictionary<int, Territory> regions;

        public InformationDatasets()
        {
            TerritoryInformationParse();
        }

        private void TerritoryInformationParse()
        {
            string[] lineSplit = FileReadIn.TerritoryInformation().Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            string[] names = new string[lineSplit.Length];
            string[] territoryRGBStrings = new string[lineSplit.Length];
            string[][] borders = new string[lineSplit.Length][];
            int index = 0;
            foreach (string line in lineSplit)
            {
                string[] tabSplit = line.Split('\t');
                if (tabSplit.Length < 3) throw new Exception($"Invalid data on line {index + 1}: {line[index]}");
                names[index] = tabSplit[0];
                territoryRGBStrings[index] = tabSplit[1];
                borders[index] = tabSplit[2].Split(',');
            }
        }

        private void FillTerritoryLookup(string[] names, int[] regionIDs, string[][] borders)
        {

        }
    }
}
