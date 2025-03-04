using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

using SynchronousRisk;

namespace SynchronousRisk.PhaseProcessing
{
	/* Russell Phillips
	   2/10/2025
	   class for managing attacks */
    public class AttackPhase : Phases
    {
        private Random rand = new Random();
		
		List<int> attackerRolls = new List<int>();
		
		List<int> defenderRolls = new List<int>();
		
        public AttackPhase(Territory[] allTerrs, Player currPlay, Board actBoar) : base(allTerrs, currPlay, actBoar)
        {

        }

        // Returns the result of 6 sided dice
        private int d6()
        {
            return rand.Next(1, 7);
        }

        // Does one step of battle, subtracting lost troops
        private void battle(int attackers, Territory attackerTerritory, Territory defenderTerritory)
        {
            int defenders = defenderTerritory.GetTroops();
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
					attackerTerritory.SetTroops(attackerTerritory.GetTroops() - 1);
				}
				else
				{
					defenderTerritory.SetTroops(defenderTerritory.GetTroops() - 1);
				}
            }

        }

        void Phase(Player attackingPlayer)
        {
            Console.WriteLine("Input territory to attack from");
            Territory attackerTerritory = null;

            while (attackerTerritory == null)
            {
                attackerTerritory = GetUserInputTerritory();
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
                defenderTerritory = GetUserInputTerritory();
                if (attackingPlayer.OwnedTerritories.Contains(defenderTerritory))
                {
                    Console.WriteLine("Sorry, you can't attack yourself");
                    defenderTerritory = null;
                }
            }

            int attackers = -1;

            while (attackers != 0 )
            {
                Console.WriteLine("input number to attack with (0 to stop attacking)");
                attackers = GetUserInputNumber(0, attackerTerritory.GetTroops());

                battle(attackers, attackerTerritory, defenderTerritory);

                WriteRoles();


                Console.WriteLine($"You now have {attackerTerritory.GetTroops()} troops left, and the defender has {defenderTerritory.GetTroops()} troops left");
            }

            Console.WriteLine("attack somewhere else? 1 for yes, 0 for no");
            int cont = GetUserInputNumber(0, 1);
            
            if (cont == 1)
            {
                Phase(attackingPlayer);
            }

        }

        private void WriteRoles()
        {
            Console.WriteLine("Your roles were: ");
            Console.WriteLine(attackerRolls);
            Console.WriteLine("The defenders roles were: ");
            Console.WriteLine(defenderRolls);
        }

    }
}