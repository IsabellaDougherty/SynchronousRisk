using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk;
using SynchronousRisk.Menus;

namespace SynchronousRisk.PhaseProcessing
{
    public class FortifyPhase : Phases
    {

        Territory SourceTerritory;
        Territory DestinationTerritory;


        public FortifyPhase(GameState g) : base(g)
        {
            CanContinue = true;

        }
        public override UIManager Start()
        {
            gameState.PhaseInt = 3;
            return new SelectTerritory("Input Territory to foritify from", GetSourceTerritory);
        }

        public UIManager GetSourceTerritory(Territory sourceTerr)
        {
            SourceTerritory = sourceTerr;
            if (SourceTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetSourceTerritory);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(SourceTerritory))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetSourceTerritory);
            }

            if (SourceTerritory.GetTroops() < 2)
            {
                return new SelectTerritory("Sorry, you don't have enough troops there", GetSourceTerritory);
            }

            return new SelectTerritory("Input territory to fortify to", GetDestinationTerritory);
        }
        public UIManager GetDestinationTerritory(Territory destinationTerr)
        {
            DestinationTerritory = destinationTerr;

            if (DestinationTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetDestinationTerritory);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(DestinationTerritory))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetDestinationTerritory);
            }

            return new SelectNumber($"Input number to transfer", GetNumTransfer, 0, SourceTerritory.GetTroops() - 1);
        }

        public UIManager GetNumTransfer(int numTransfer)
        {
            if (numTransfer > SourceTerritory.GetTroops() - 1 || numTransfer < 0) // have to leave one troop behind
            {
                //return new UIManager("Sorry, invalid number of troops", GetNumTransfer);
            }
            SourceTerritory.SetTroops(SourceTerritory.GetTroops() - numTransfer);
            DestinationTerritory.SetTroops(DestinationTerritory.GetTroops() + numTransfer);

            return new SelectTerritory("Input Territory to foritify from", GetSourceTerritory);

        }
    }
}

