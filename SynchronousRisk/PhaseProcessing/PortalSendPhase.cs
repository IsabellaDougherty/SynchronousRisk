using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{
    /// Karen Dixon 4/28/2025
    /// <summary>
    /// Phase representing the Portal Send Phase at the end of a player's turn
    /// </summary>
    internal class PortalSendPhase : Phase
    {
        Territory sourceTerritory;

        internal PortalSendPhase(GameState g) : base(g)
        {
            CanContinue = true;
        }
        /// Karen Dixon 4/28/2025
        /// <summary>
        /// Start of the Portal Send Phase
        /// </summary>
        /// <returns></returns>
        public override UIManager Start()
        {
            gameState.PhaseInt = 4;
            gameState.phaseChange = true;
            return new SelectTerritory("Input portal to send troops through", GetSourceTerritory);
        }
        /// Karen Dixon 4/28/2025
        /// <summary>
        /// Gets the territory with the portal that the player will send troops through
        /// </summary>
        /// <param name="sourceTerr"></param>
        /// <returns></returns>
        public UIManager GetSourceTerritory(Territory sourceTerr)
        {
            if (sourceTerr == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetSourceTerritory);
            }
            if (!CurrentPlayer.OwnedTerritories.Contains(sourceTerr))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetSourceTerritory);
            }
            if (sourceTerr.GetTroops() < 2)
            {
                return new SelectTerritory("Sorry, you don't have enough troops there", GetSourceTerritory);
            }
            if (!sourceTerr.PortalPresent)
            {
                return new SelectTerritory("Sorry, that territory does not have a portal", GetSourceTerritory);

            }
            sourceTerritory = sourceTerr;
            return new SelectNumber($"Input number to transfer", GetNumTransfer, 0, sourceTerritory.GetTroops() - 1);
        }
        /// Karen Dixon 4/28/2025
        /// <summary>
        /// Transfers troops to the portal's troop storage
        /// </summary>
        /// <param name="numTransfer"></param>
        /// <returns></returns>
        public UIManager GetNumTransfer(int numTransfer)
        {
            sourceTerritory.SetTroops(sourceTerritory.GetTroops() - numTransfer);
            sourceTerritory.ExitPortal.TransferTroops(CurrentPlayer, numTransfer);
            return new SelectTerritory("Input portal to send troops through", GetSourceTerritory);
        }
    }
}
