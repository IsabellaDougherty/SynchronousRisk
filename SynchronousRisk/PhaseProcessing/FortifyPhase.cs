using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk;

namespace SynchronousRisk.PhaseProcessing
{
    public class FortifyPhase
    {

        public FortifyPhase() { }

        void Phase(Player fortifyingPlayer)
        {
            Console.WriteLine("Input territory to fortify from");
            Territory sourceTerritory = null;

            while (sourceTerritory == null)
            {
                sourceTerritory = InputTerritory();
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
                destinationTerritory = InputTerritory();
                if (!fortifyingPlayer.OwnedTerritories.Contains(destinationTerritory))
                {
9                   Console.WriteLine("Sorry, you don't own that territory");
                    destinationTerritory = null;
                }
            }


            Console.WriteLine("input number to transfer");
            int numberTransfer = InputInteger(0, sourceTerritory.Troops);

            sourceTerritory.Troops -= numberTransfer;
            destinationTerritory.Troops -= numberTransfer;

            Console.WriteLine($"The first territory now has {atackerTerritory.Troops} troops left, and the second territory has {destinationTerritory.Troops} troops")

            Console.WriteLine("Fortify somewhere else? 1 for yes, 0 for no";
            int cont = InputInteger(0, 1);
            
            if (cont == 1)
            {
                Phase();
            }

        }

        private bool skip()
        {
            return true;
        }

        private Territory InputTerritory()
        {
            return new Territory("", 1, new string[3]);
        }

        private int InputInteger(int min, int max)
        {
            return 3;
        }


    }
}
