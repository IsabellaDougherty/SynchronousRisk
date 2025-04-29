using SynchronousRisk.Menus;
using SynchronousRisk.obj.Game_Pieces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SynchronousRisk.PhaseProcessing
{
    internal class PortalReceivePhase : Phase
    {
        public PortalReceivePhase(GameState g) : base(g) 
        {
            CanContinue = true;
        }
        public override UIManager Start()
        {
            gameState.PhaseInt = 0;
            gameState.phaseChange = true;
            String message = "No troops to receive";

            // Russell Phillips 4/21/2025
            foreach (Portal portal in gameState.Portals)
            {
                foreach ((Player player, int numTroops) in portal.Transit)
                {
                    if (player == CurrentPlayer)
                    {
                        if (!player.OwnedTerritories.Contains(portal.Destination))
                        {
                            Territory dummyTerritory = new Territory(numTroops);
                            AttackPhase attackPhase = new AttackPhase(gameState, dummyTerritory, portal.Destination);
                            while (dummyTerritory.GetTroops() != 0)
                            {
                                attackPhase.Battle(Math.Min(dummyTerritory.GetTroops(), 3));
                                if (attackPhase.CheckBattleWon(player))
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
                            portal.Destination.SetTroops(portal.Destination.GetTroops() + numTroops);
                            message = "Troops recieved";
                        }
                    }
                }
            }
            return new UIManager(message, false);
        }
    }
}
