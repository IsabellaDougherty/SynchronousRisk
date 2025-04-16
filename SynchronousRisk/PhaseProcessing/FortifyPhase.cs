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

        Phases NextPhase;

        public FortifyPhase(GameState g) : base(g)
        {

        }
        public override UIManager Start()
        {
            gameState.PhaseInt = 3;
            NextPhase = new DraftPhase(gameState);
            return new SelectTerritory("Input Territory to foritify from", GetSourceTerritory, NextPhase);
        }

        public UIManager GetSourceTerritory(Territory sourceTerr)
        {
            SourceTerritory = sourceTerr;
            if (SourceTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetSourceTerritory, NextPhase);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(SourceTerritory))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetSourceTerritory, NextPhase);
            }

            if (SourceTerritory.GetTroops() < 2)
            {
                return new SelectTerritory("Sorry, you don't have enough troops there", GetSourceTerritory, NextPhase);
            }

            return new SelectTerritory("Input territory to fortify to", GetDestinationTerritory, NextPhase);
        }
        public UIManager GetDestinationTerritory(Territory destinationTerr)
        {
            DestinationTerritory = destinationTerr;

            if (DestinationTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetDestinationTerritory, NextPhase);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(DestinationTerritory))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetDestinationTerritory, NextPhase);
            }

            return new SelectNumber($"Input number to transfer", GetNumTransfer, 0, SourceTerritory.GetTroops() - 1, NextPhase);
        }

        public UIManager GetNumTransfer(int numTransfer)
        {
            if (numTransfer > SourceTerritory.GetTroops() - 1 || numTransfer < 0) // have to leave one troop behind
            {
                //return new UIManager("Sorry, invalid number of troops", GetNumTransfer);
            }
            SourceTerritory.SetTroops(SourceTerritory.GetTroops() - numTransfer);
            DestinationTerritory.SetTroops(DestinationTerritory.GetTroops() + numTransfer);

            return new SelectTerritory("Input Territory to foritify from", GetSourceTerritory, NextPhase);

        }
    }
}

