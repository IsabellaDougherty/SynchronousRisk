using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk;

namespace SynchronousRisk.PhaseProcessing
{
    public class FortifyPhase : Phases
    {

        public FortifyPhase(Territory[] allTerrs, Player currPlay, Board actBoar) : base(allTerrs, currPlay, actBoar)
        {

        }

        void Phase(Player fortifyingPlayer)
        {
            Console.WriteLine("Input territory to fortify from");
            Territory sourceTerritory = null;

            while (sourceTerritory == null)
            {
                sourceTerritory = GetUserInputTerritory();
                if (!fortifyingPlayer.OwnedTerritories.Contains(sourceTerritory))
                {
                    Console.WriteLine("Sorry, you don't own that territory");
                    sourceTerritory = null;
                }
            }

            Console.WriteLine("Input territory to fortify to");
            Territory destinationTerritory = null;
            
            while (destinationTerritory == null)
            {
                destinationTerritory = GetUserInputTerritory();
                if (!fortifyingPlayer.OwnedTerritories.Contains(destinationTerritory))
                {
                    Console.WriteLine("Sorry, you don't own that territory");
                    destinationTerritory = null;
                }
            }


            Console.WriteLine("input number to transfer");
            int numberTransfer = GetUserInputNumber(0, sourceTerritory.GetTroops());

            sourceTerritory.SetTroops(sourceTerritory.GetTroops() - numberTransfer);
            destinationTerritory.SetTroops(destinationTerritory.GetTroops() - numberTransfer);

            Console.WriteLine($"The first territory now has {destinationTerritory.GetTroops()} troops left, and the second territory has {destinationTerritory.GetTroops()} troops");

            Console.WriteLine("Fortify somewhere else? 1 for yes, 0 for no");
            int cont = GetUserInputNumber(0, 1);
            
            if (cont == 1)
            {
                Phase(fortifyingPlayer);
            }

        }

    }
}
