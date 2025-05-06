using SynchronousRisk.Menus;
using SynchronousRisk.obj.Game_Pieces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SynchronousRisk.PhaseProcessing
{
    /// Karen Dixon 4/28/2025
    /// <summary>
    /// Phase representing the Portal Recieve Phase at the start of a player's turn
    /// </summary>
    internal class PortalReceivePhase : Phase
    {
        public PortalReceivePhase(GameState g) : base(g) 
        {
            CanContinue = true;
        }
        /// Karen Dixon 4/28/2025
        /// <summary>
        /// Start of the Portal Receive Phase
        /// </summary>
        /// <returns></returns>
        public override UIManager Start()
        {
            gameState.PhaseInt = 0;
            gameState.phaseChange = true;
            String message = "No troops to receive";

            // Russell Phillips & Karen Dixon 4/21/2025 
            // Iterates through portals and (player, troop count) pairs
            foreach (Portal portal in gameState.Portals)
            {
                for (int i = portal.Transit.Count - 1; i >= 0; i--)
                {
                    if (portal.Transit[i].Item1 == CurrentPlayer)
                    {
                        if (!portal.Transit[i].Item1.OwnedTerritories.Contains(portal.Destination))
                        {
                            Territory dummyTerritory = new Territory(portal.Transit[i].Item2);
                            AttackPhase attackPhase = new AttackPhase(gameState, dummyTerritory, portal.Destination);
                            while (dummyTerritory.GetTroops() != 0)
                            {
                                attackPhase.Battle(Math.Min(dummyTerritory.GetTroops(), 3));
                                if (attackPhase.CheckBattleWon(portal.Transit[i].Item1))
                                {
                                    portal.Destination.SetTroops(dummyTerritory.GetTroops());
                                    message = "Battle won";
                                    break;
                                }
                            }
                            message = "Troops lost";
                        }
                        else
                        {
                            portal.Destination.SetTroops(portal.Destination.GetTroops() + portal.Transit[i].Item2);
                            message = "Troops recieved";
                        }
                        portal.Transit.RemoveAt(i);
                    }
                }
            }
            return new UIManager(message, false);
        }
    }
}
