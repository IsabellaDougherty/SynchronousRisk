using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk.PhaseProcessing;

namespace SynchronousRisk.obj.Game_Pieces
{
    /// Russell Phillips 4/21/2025
    /// <summary>
    /// Object that manages one direction of wormhole travel
    /// </summary>
    public class Portal
    {
        public Territory Source;
        public Territory Destination;

        public List<(Player, int)> Transit;

        /// Russell Phillips 4/21/2025
        /// <summary>
        /// Make a wormhole with a source and destination territory.
        /// </summary>
        public Portal(Territory source, Territory destination)
        {
            Source = source;
            Destination = destination;
            Transit = new List<(Player, int)>();
        }

        /// Russell Phillips 4/21/2025
        /// <summary>
        /// Start the transfer of troops from the source territory to the destination
        /// </summary>
        public void TransferTroops(Player player, int numTroops)
        {
            Transit.Add((player, numTroops));
        }

        /// Russell Phillips 4/21/2025
        /// <summary>
        /// Completes the transfer of troops from the source territory to the destination
        /// </summary>
        /// may need to be moved to the actual portal receive phase
        public void resolve(GameState gameState)
        {
            foreach ((Player player, int numTroops) in Transit)
            {
                Territory dummyTerritory = new Territory(numTroops);
                AttackPhase attackPhase = new AttackPhase(gameState, dummyTerritory, Destination);
                while (dummyTerritory.GetTroops() != 0)
                {
                    attackPhase.Battle(Math.Min(dummyTerritory.GetTroops(), 3));
                    if (attackPhase.CheckBattleWon(player))
                    {
                        Destination.SetTroops(dummyTerritory.GetTroops());
                        break;  
                    }
                }
            }

        }
    }
}
