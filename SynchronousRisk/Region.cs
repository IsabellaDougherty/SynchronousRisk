using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    /// <summary>
    /// IAD 2/21/2025
    /// Object class to represent a region of territories to assist in troop allocation as well as limiting of number of portals that can be placed in a singular region.
    /// </summary>
    public class Region
    {
        public int regionID;
        public Territory[] territoriesInRegion; int regionBonus;
        public static bool portalPresent = false; //IAD 2/21/2025: To be utilized later when portals are implimented. Will be for a check to assist in ensuring multiple portals don't open in the same place unless that is desired.
        public Region(int regionID, Territory[] territories)
        {
            this.regionID = regionID;
            territoriesInRegion = territories;
            regionBonus = territoriesInRegion.Length /2;
            //Console.WriteLine("Region " + regionID + " created with " + territoriesInRegion.Length + " territories and a bonus of " + regionBonus);
        }
        
        public bool AllTerritoriesOwnedByPlayer(Player player)
        {
            Territory[] playerTerritories = player.OwnedTerritories.ToArray();
            for (int i = 0; i < territoriesInRegion.Length; i++)
            {
                if (!playerTerritories.Contains(territoriesInRegion[i]))
                {
                    return false;
                }
                else return true;
            }
            return true;
        }
    }
}