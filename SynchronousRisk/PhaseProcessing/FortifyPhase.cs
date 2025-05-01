using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk;
using SynchronousRisk.Menus;

namespace SynchronousRisk.PhaseProcessing
{
    /// Russell Phillips 3/4/2025
    /// <summary>
    /// Phase that represents the forify phase of risk
    /// </summary>
    public class FortifyPhase : Phase
    {

        Territory SourceTerritory;
        Territory DestinationTerritory;


        public FortifyPhase(GameState g) : base(g)
        {
            CanContinue = true;

        }

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Beginning of the fortify phase
        /// </summary>
        /// <returns>UImanager</returns>
        public override UIManager Start()
        {
            gameState.PhaseInt = 3;
            gameState.phaseChange = true;
            return new SelectTerritory("Input Territory to foritify from", GetSourceTerritory);
        }

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Gets the territory where the troops will come from 
        /// </summary>
        /// <param name="sourceTerr">territory the troops will be taken from</param>
        /// <returns>UImanager</returns>
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

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Gets the territory the troops will be placed on
        /// </summary>
        /// <param name="destinationTerr">territory troops will be placed on</param>
        /// <returns>UImanager</returns>
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

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Transfers a number of troops
        /// </summary>
        /// <param name="numTransfer">the number of troops to transfer</param>
        /// <returns>UImanager</returns>
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

