using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

using SynchronousRisk;

namespace SynchronousRisk.PhaseProcessing
{
	/* Russell Phillips
	   2/10/2025
	   class for managing attacks */
    public class AttackPhase
    {
        private Random rand = new Random();
		
		List<int> attackerRolls = new List<int>();
		
		List<int> defenderRolls = new List<int>();
		
        public AttackPhase()
        {

        }

        // returns the result of 6 sided dice
        private int d6()
        {
            return rand.Next(1, 7);
        }

        // does one step of battle, subtracting lost troops
        private void battle(int attackers, Territory attackerTerritory, Territory defenderTerritory)
        {
            int defenders = defenderTerritory.Troops;
            attackerRolls = new List<int>();
            defenderRolls = new List<int>();

            for (int i = 0; i < attackers; i++)
            {
                attackerRolls.Add(d6());
            }

            for (int i = 0; i < defenders; i++)
            {
                defenderRolls.Add(d6());
            }

            attackerRolls.Sort((x, y) => y.CompareTo(x));
            defenderRolls.Sort((x, y) => y.CompareTo(x));

            for (int i = 0; i < Math.Min(attackers, defenders); i++)
            {
                if (defenderRolls[i] >= attackerRolls[i])
				{
					attackerTerritory.Troops -= 1;
				}
				else
				{
					defenderTerritory.Troops -= 1;
				}
            }

        }

        void Phase(Player attackingPlayer)
        {
            Console.WriteLine("Input territory to attack from");
            Territory attackerTerritory = null;

            while (attackerTerritory == null)
            {
                attackerTerritory = InputTerritory();
                if (!attackingPlayer.OwnedTerritories.Contains(attackerTerritory))
                {
                    Console.WriteLine("Sorry, you don't own that territory");
                    attackerTerritory = null;
                }
            }

            Console.WriteLine("Input territory to attack");
            Territory defenderTerritory = null;
            
            while (defenderTerritory == null)
            {
                defenderTerritory = InputTerritory();
                if (attackingPlayer.OwnedTerritories.Contains(defenderTerritory))
                {
9                   Console.WriteLine("Sorry, you can't attack yourself");
                    defenderTerritory = null;
                }
            }

            int attackers -1;

            while (attackers != 0 )
            {
                Console.WriteLine("input number to attack with (0 to stop attacking)");
                attackers = InputInteger(0, attackerTerritory.Troops);

                battle(attackers, attackerTerritory, defenederTerritory);

                WriteRoles()
                   
                Console.WriteLine($"You now have {atackerTerritory.Troops} troops left, and the defender has {defenderTerritory.Troops} troops left")
            }

            Console.WriteLine("attack somewhere else? 1 for yes, 0 for no";
            int cont = InputInteger(0, 1);
            
            if (cont == 1)
            {
                Phase();
            }

        }

        private void WriteRoles()
        {
            Console.WriteLine("Your roles were: ");
            Console.WriteLine(attackerRolls);
            Console.WriteLine("The defenders roles were: ");
            Console.WriteLine(defenderRolls);
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