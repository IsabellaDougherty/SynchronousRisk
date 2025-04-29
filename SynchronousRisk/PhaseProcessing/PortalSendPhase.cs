using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{
    internal class PortalSendPhase : Phase
    {
        Territory sourceTerritory;

        internal PortalSendPhase(GameState g) : base(g)
        {
            CanContinue = true;
        }
        public override UIManager Start()
        {
            gameState.PhaseInt = 4;
            gameState.phaseChange = true;
            return new SelectTerritory("Input portal to send troops through", GetSourceTerritory);
        }
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
        public UIManager GetNumTransfer(int numTransfer)
        {
            sourceTerritory.SetTroops(sourceTerritory.GetTroops() - numTransfer);
            sourceTerritory.ExitPortal.TransferTroops(CurrentPlayer, numTransfer);
            return new SelectTerritory("Input portal to send troops through", GetSourceTerritory);
        }
    }
}
