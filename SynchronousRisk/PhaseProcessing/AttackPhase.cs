using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography.X509Certificates;
using SynchronousRisk;
using SynchronousRisk.Menus;

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

        Territory AttackerTerritory;
        Territory DefenderTerritory;
		
        public AttackPhase(Player currPlay, Board actBoar) : base(currPlay, actBoar)
        {

        }

        // Returns the result of 6 sided dice
        private int d6()
        {
            return rand.Next(1, 7);
        }

        // Does one step of battle, subtracting lost troops
        // does one step of battle, subtracting lost troops
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
                Console.WriteLine(attackerRolls[i]);
                Console.WriteLine(defenderRolls[i]);
                if (defenderRolls[i] >= attackerRolls[i])
                { 
					AttackerTerritory.SetTroops(AttackerTerritory.GetTroops() - 1);
				}
				else
				{
					DefenderTerritory.SetTroops(DefenderTerritory.GetTroops() - 1);
				}
            }

        }
        public override UIManager Start()
        {
            return new UIManager("Input Territory to attack from", GetAttackerTerritory);
        }

        public UIManager GetAttackerTerritory(string inp)
        {
            AttackerTerritory = GetTerritory(inp);
            if (AttackerTerritory == null)
            {
                return new UIManager("Sorry, couldn't find that territory, try again", GetAttackerTerritory);
            }

            if (!currentPlayer.OwnedTerritories.Contains(AttackerTerritory))
            {
                return new UIManager("Sorry, you don't own that territory", GetAttackerTerritory);
            }

            if (AttackerTerritory.GetTroops() < 2)
            {
                return new UIManager("Sorry, you don't have enough troops there", GetAttackerTerritory);
            }

            return new UIManager("Input territory to attack", GetDefenderTerritory);
        }
        public UIManager GetDefenderTerritory(string inp)
        {

            DefenderTerritory = GetTerritory(inp);
            
            if (DefenderTerritory == null)
            {
                return new UIManager("Sorry, couldn't find that territory, try again", GetDefenderTerritory);
            }

            if (currentPlayer.OwnedTerritories.Contains(DefenderTerritory))
            {
                return new UIManager("Sorry, you can't attack yourself", GetDefenderTerritory);
            }

            return new UIManager("Input number to attack with (0 to stop attacking)", GetNumAttackers);
        }

        public UIManager GetNumAttackers(string inp)
        {
            int attackers = int.Parse(inp);
            if (attackers > AttackerTerritory.GetTroops() - 1 || attackers < 0 || attackers > 3) // have to leave one troop behind
            {
                return new UIManager("Sorry, invalid number of troops", GetNumAttackers);
            }
            battle(attackers);
            WriteRolls();
            string output = $"You now have {AttackerTerritory.GetTroops()} troops left, and the defender has {DefenderTerritory.GetTroops()} troops left ";
            
            if (AttackerTerritory.GetTroops() <= 1)
            {
                return new UIManager(output + "You no longer have enough troops to attack. Attack somewhere else? 1 for yes, 0 for no", Continue);
            }

            if (attackers > 0)
            {
                return new UIManager(output + "Input number to attack with (0 to stop attacking)", GetNumAttackers);
            }
            return new UIManager("Attack somewhere else? 1 for yes, 0 for no", Continue);
        }
        public UIManager Continue(string inp)
        {
            int cont = int.Parse(inp);
            
            if (cont == 1)
            {
                return new UIManager("Input territory to attack from", GetAttackerTerritory);
            }

            return new UIManager();
        }

        private void WriteRolls()
        {
            Console.WriteLine("Your rolls were: ");
            Console.WriteLine(string.Join(" ", attackerRolls));
            Console.WriteLine("The defenders rolls were: ");
            Console.WriteLine(string.Join(" ", defenderRolls));
        }

    }
}