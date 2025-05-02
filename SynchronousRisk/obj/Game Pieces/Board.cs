using SynchronousRisk.PhaseProcessing;
using SynchronousRisk.Resources.Assets.Text_Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SynchronousRisk.Menus;

namespace SynchronousRisk
{
    /// IAD 2/12/2025 <summary> Class to generate all territories for a board along with all required information for each territory. </summary>
    public class Board
    {
        public InformationDatasets infoData = new InformationDatasets();
        private Region[] regionsArray;
        private Territory[] allTerritories;

        public LinkedList<Phase> Phases;
        public UIManager CurrMenu;

        private Random Rand;
        /// IAD 2/12/2025 <summary> Constructor for the Board class. </summary>
        public Board(GameState g, Random rand)
        {
            regionsArray = MakeRegions();
            allTerritories = infoData.GetTerritoriesArray();
            Phases = new LinkedList<Phase>();
            Rand = rand;
        }
        /// IAD 3/17/2025 <summary> Creates a region object for each region in the game. </summary>
        public Region[] MakeRegions()
        {
            List<int> regionIDs = new List<int>();
            List<Region> regionsMade = new List<Region>();
            foreach (int[] rgb in infoData.rgbLookup.Keys)
            {
                if (!regionIDs.Contains(rgb[1])) 
                { 
                    regionIDs.Add(rgb[1]);
                    Region region = new Region(rgb[1], new Territory[0]);
                    regionsMade.Add(region);
                    foreach (Territory t in infoData.rgbLookup.Values)
                    {
                        if (t.rgb[1] == rgb[1]) { region.AddTerritory(t); }
                    }
                }
            }
            return regionsMade.ToArray();
        }
        /// IAD 3/17/2025 <summary> Returns the regions in the game. </summary>
        public Region[] GetRegions() { return regionsArray; }
        /// IAD 2/12/2025 <summary> Returns the territories in the game. </summary>
        public Territory[] GetTerritories()
        {
            return infoData.GetTerritoriesArray();
        }
        /// IAD 2/12/2025 <summary> Returns the territory object with the given name. </summary> <param name="name"></param>
        public Territory GetTerritoryByName(string name)
        {
            if (infoData.territoryLookup.TryGetValue(name, out var territory))
                return territory;
            return null;
        }
        /// IAD 2/12/2025 <summary> Returns the territories that border the given territory. </summary> <param name="territoryName"></param>
        public List<Territory> GetBorders(string territoryName)
        {
            if (infoData.borders.TryGetValue(territoryName, out var borderList))
                return borderList;
            return new List<Territory>();
        }
        /// IAD 2/12/2025 <summary> Returns all territories in the game. </summary>
        public IReadOnlyDictionary<string, Territory> GetAllTerritories() { return infoData.territoryLookup; }
        /// IAD 2/12/2025 <summary> Returns a string that can function as a display for all territories and their borders. </summary>
        public string DisplayBoard()
        {
            Dictionary<int, List<Territory>> territoriesByRegion = new Dictionary<int, List<Territory>>();
            foreach (var territory in infoData.territoryLookup.Values)
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

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Distrubute territories to players on board
        /// </summary>
        /// <param name="players">players on the current board</param>
        public void DistributeTerritories(List<Player> players, GameState g)
        {
                List<Territory> territories = new List<Territory>(GetTerritories());
                shuffle(territories);

                for (int i = 0; i < territories.Count; i++)
                {
                    players[i % players.Count()].OwnedTerritories.Add(territories[i]);
                    territories[i].SetTroops(1);
                }

            Phases.AddLast(new SetupPhase(g, players, 1));
            CurrMenu = GetCurrentPhase().Start();
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// shuffles a generic list in place
        /// </summary>
        /// <typeparam name="T">type of list to be shuffled</typeparam>
        /// <param name="lst">list to be shuffled</param>
        /// <returns>shuffled list</returns>
        private List<T> shuffle<T>(List<T> lst)
        {
            for (int i = lst.Count - 1; i > 0; i--)
            {
                int j = Rand.Next(i + 1);
                T value = lst[j];
                lst[j] = lst[i];
                lst[i] = value;
            }
            return lst;
        }

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Moves on to the next phase
        /// </summary>
        public void NextPhase()
        {
            if (Phases.Count() == 0)
            {
                CurrMenu = new UIManager("Either click end turn button or check other maps for moves to make", false);
                return;
            }

            if (GetCurrentPhase() is AttackPhase ap)
            {
                if (ap.BattleWon)
                {
                    ap.DrawCard();
                }
            }

            Phases.RemoveFirst();

            if (Phases.Count() == 0)
            {
                CurrMenu = new UIManager("Either click end turn button or check other maps for moves to make", false);
                return;
            }

            CurrMenu = Phases.First.Value.Start();
        }

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Gets the current phase
        /// </summary>
        /// <returns>current phase</returns>
        public Phase GetCurrentPhase()
        {
            if (Phases.Count() == 0)
            {
                return new Phase();
            }
            return Phases.First.Value;
        }

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Checks if all phases are finished
        /// </summary>
        /// <returns>if all phases finished</returns>
        public bool CanEndTurn()
        {
            return Phases.Count == 0;
        }

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Creates phases for the current turns player if they own territories on this board
        /// </summary>
        public void CreatePhases(GameState gameState)
        {
            if (PlayerOwnsTerritory(gameState, gameState.GetCurrentTurnsPlayer()))
            {
                Phases.AddLast(new PortalReceivePhase(gameState));
                Phases.AddLast(new DraftPhase(gameState));
                Phases.AddLast(new AttackPhase(gameState));
                Phases.AddLast(new FortifyPhase(gameState));
                Phases.AddLast(new PortalSendPhase(gameState));

                CurrMenu = Phases.First.Value.Start();
            }
        }

        /// Russell Phillips 4/27/2025
        /// <summary>
        /// Checks if a player owns territory on this board
        /// </summary>
        /// <param name="player">Player being checked</param>
        /// <returns>if player owns territory on this board</returns>
        public bool PlayerOwnsTerritory(GameState gameState, Player player)
        {
            foreach(Territory territory in GetTerritories())
            {
                foreach(Territory playerTerritory in player.OwnedTerritories)
                {
                    if (territory == playerTerritory)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}