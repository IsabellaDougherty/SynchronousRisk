using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SynchronousRisk
{
    //IAD 2/12/2025: Class to generate all territories for a board along with all required information for each territory.
    public class Board
    {
        private Dictionary<string, Territory> territoryLookup;
        private Dictionary<string, List<Territory>> borders;
        private Dictionary<int, Territory> regions;
        public List<Region> allRegions = new List<Region>();

        public Board()
        {
            Territories allTerritories = new Territories();
            Territory[] territoryArray = allTerritories.GetTerritories();
            territoryLookup = new Dictionary<string, Territory>();
            borders = new Dictionary<string, List<Territory>>();
            foreach (var territory in territoryArray) { territoryLookup[territory.GetName()] = territory; }
            foreach (var territory in territoryArray)
            {
                string territoryName = territory.GetName();
                List<Territory> borderNeighbors = new List<Territory>();
                foreach (var borderName in territory.GetBorders())
                {
                    if (territoryLookup.ContainsKey(borderName))
                    {
                        borderNeighbors.Add(territoryLookup[borderName]);
                    }
                    else
                    { Console.WriteLine($"Warning: Territory '{borderName}' (border of '{territoryName}') not found."); }
                }
                borders[territoryName] = borderNeighbors;
            }
            MakeRegions();
        }
        public void MakeRegions()
        {
            regions = new Dictionary<int, Territory>();
            foreach (Territory t in territoryLookup.Values)
            { if (!regions.ContainsKey(t.GetRegionID())) { regions[t.GetRegionID()] = t; } }
            foreach (int r in regions.Keys)
            {
                Territory[] territoriesInRegion = territoryLookup.Values
                    .Where(t => t.GetRegionID() == r)
                    .ToArray();
                Region newRegion = new Region(r, territoriesInRegion);
                allRegions.Add(newRegion);
            }
        }
        public Territory GetTerritoryByName(string name)
        {
            if (territoryLookup.TryGetValue(name, out var territory))
                return territory;
            return null;
        }
        public List<Territory> GetBorders(string territoryName)
        {
            if (borders.TryGetValue(territoryName, out var borderList))
                return borderList;
            return new List<Territory>();
        }
        public IReadOnlyDictionary<string, Territory> GetAllTerritories() { return territoryLookup; }
        public string DisplayBoard()
        {
            Dictionary<int, List<Territory>> territoriesByRegion = new Dictionary<int, List<Territory>>();
            foreach (var territory in territoryLookup.Values)
            {
                int region = territory.GetRegionID();
                if (!territoriesByRegion.ContainsKey(region))
                    territoriesByRegion[region] = new List<Territory>();
                territoriesByRegion[region].Add(territory);
            }
            List<int> sortedRegions = territoriesByRegion.Keys.ToList();
            sortedRegions.Sort();

            StringBuilder strBuilder= new StringBuilder();
            strBuilder.AppendLine("=== Board Visualization by Region ===");

            foreach (var region in sortedRegions)
            {
                strBuilder.AppendLine($"Region {region}:");
                List<Territory> territoriesInRegion = territoriesByRegion[region]
                                                        .OrderBy(t => t.GetName())
                                                        .ToList();

                foreach (var territory in territoriesInRegion)
                {
                    strBuilder.AppendLine($"  Territory: {territory.GetName()}");
                    List<Territory> borderTerritories = GetBorders(territory.GetName());
                    Dictionary<int, List<string>> bordersByRegion = new Dictionary<int, List<string>>();
                    foreach (var border in borderTerritories)
                    {
                        int borderRegion = border.GetRegionID();
                        if (!bordersByRegion.ContainsKey(borderRegion))
                            bordersByRegion[borderRegion] = new List<string>();
                        bordersByRegion[borderRegion].Add(border.GetName());
                    }
                    List<int> sortedBorderRegions = bordersByRegion.Keys.ToList();
                    sortedBorderRegions.Sort();

                    foreach (var bRegion in sortedBorderRegions)
                    {
                        List<string> borderNames = bordersByRegion[bRegion];
                        borderNames.Sort();
                        strBuilder.AppendLine($"    Region {bRegion} Borders: {string.Join(", ", borderNames)}");
                    }
                }
            }
            strBuilder.AppendLine("=== End of Board Visualization ===");
            return strBuilder.ToString();
        }
    }
}