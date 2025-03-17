using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    /// IAD 2/21/2025 <summary>
    /// Object class to represent a region of territories to assist in troop allocation as well as limiting of number of portals that can be placed in a singular region.
    /// </summary>
    public class Region
    {
        /// IAD 2/21/2025 <summary>
        /// Variables for the region class.
        /// </summary>
        public int regionID;
        int regionBonus;
        public Territory[] territoriesInRegion; 
        public static bool portalPresent = false; //IAD 2/21/2025: To be utilized later when portals are implimented. Will be for a check to assist in ensuring multiple portals don't open in the same place unless that is desired.
        /// IAD 2/21/2025 <summary>
        /// Constructor for the Region class.
        /// </summary>
        /// <param name="regionID"></param>
        /// <param name="territories"></param>
        public Region(int regionID, Territory[] territories)
        {
            this.regionID = regionID;
            territoriesInRegion = territories;
            regionBonus = territoriesInRegion.Length /2;
            //Console.WriteLine("Region " + regionID + " created with " + territoriesInRegion.Length + " territories and a bonus of " + regionBonus);
        }
        /// IAD 2/21/2025 <summary>
        /// Returns true if all territories in the region are owned by the player provided in the parameter.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        public bool AllTerritoriesOwnedByPlayer(Player player)
        {
            Territory[] playerTerritories = player.OwnedTerritories.ToArray();
            for (int i = 0; i < territoriesInRegion.Length; i++) {
                if (!playerTerritories.Contains(territoriesInRegion[i])) { return false; }
                else return true; }
            return true;
        }
        /// IAD 2/21/2025 <summary>
        /// Returns the bonus troops for the region.
        /// </summary>
        /// <returns></returns>
        public int GetRegionBonus() { return regionBonus; }
        /// IAD 2/21/2025 <summary>
        /// Returns the territories in the region.
        /// </summary>
        /// <returns></returns>
        public Territory[] GetTerritories() { return territoriesInRegion; }
        /// IAD 2/21/2025 <summary>
        /// Returns the region ID of the region.
        /// </summary>
        /// <returns></returns>
        public int GetRegionID() { return regionID; }
    }
}