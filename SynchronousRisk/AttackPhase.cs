using System;
using System.Collections.Generic;

namespace SynchronousRisk
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
        public int d6()
        {
            return rand.Next(1, 7);
        }

        // does one step of battle, subtracting lost troops
        public void battle(int attackers, int defenders, Territory attackerTerritory, Territory defenderTerritory)
        {
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
					//attackerTerritory.troops -= 1; //troops not implemented yet
				}
				else
				{
					//defenderTerritory.troops -= 1; //troops not implemented yet
				}
            }

        }
    }
}