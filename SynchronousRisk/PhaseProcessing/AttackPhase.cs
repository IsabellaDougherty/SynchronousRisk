using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SynchronousRisk.PhaseProcessing
{
    /* Russell Phillips
	   2/10/2025
	   class for managing attacks */
    public class AttackPhase : Phase
    {
        private Random rand = new Random();

        List<int> attackerRolls = new List<int>();

        List<int> defenderRolls = new List<int>();

        Territory AttackerTerritory;
        Territory DefenderTerritory;

        public bool BattleWon = false;

        public AttackPhase(GameState g) : base(g)
        {
            CanContinue = true;
        }

        /// Russell Phillips 4/21/2025
        /// <param name="defenderTerritory"></param>
        /// <summary>
        /// AttackPhase with prespecified attacker and defender territories, for use when those are already known
        /// </summary>
        public AttackPhase(GameState g, Territory attackerTerritory, Territory defenderTerritory) : base(g)
        {
            AttackerTerritory = attackerTerritory;
            DefenderTerritory = defenderTerritory;
        }

        /// Russell Phillips 2/10/2025
        /// <summary>
        /// Returns the result of 6 sided dice
        /// </summary>
        /// <returns></returns>
        private int d6()
        {
            return rand.Next(1, 7);
        }

        /// Russell Phillips 2/10/2025
        /// <summary>
        /// does one step of battle, subtracting lost troops
        /// </summary>
        /// <param name="attackers"></param>
        public void Battle(int attackers)
        {
            int defenders = Math.Min(DefenderTerritory.GetTroops(), 2);
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

        /// Russell Phillips 2/20/2025
        /// <summary>
        /// Gets the first UImanager for the attack phase
        /// </summary>
        /// <returns></returns>
        public override UIManager Start()
        {
            gameState.PhaseInt = 2;
            gameState.phaseChange = true;
            return new SelectTerritory("Input Territory to attack from", GetAttackerTerritory);
        }

        /// Russell Phillips 2/20/2025
        /// <summary>
        /// Gets the territory that is going to attack
        /// </summary>
        /// <param name="attackerTerr">the territory that is going to attack</param>
        /// <returns>UImanager</returns>
        public UIManager GetAttackerTerritory(Territory attackerTerr)
        {
            AttackerTerritory = attackerTerr;
            if (AttackerTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetAttackerTerritory);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(AttackerTerritory))
            {
                return new SelectTerritory("Sorry, you don't own that territory", GetAttackerTerritory);
            }

            if (AttackerTerritory.GetTroops() < 2)
            {
                return new SelectTerritory("Sorry, you don't have enough troops there", GetAttackerTerritory);
            }

            return new SelectTerritory("Input territory to attack", GetDefenderTerritory);
        }

        /// Russell Phillips 2/20/2025
        /// <summary>
        /// Gets the territory that is going to be attacked
        /// </summary>
        /// <param name="defenderTerr">the territory that is going to be attacked</param>
        /// <returns>UImanager</returns>
        public UIManager GetDefenderTerritory(Territory defenderTerr)
        {
            DefenderTerritory = defenderTerr;

            if (DefenderTerritory == null)
            {
                return new SelectTerritory("Sorry, couldn't find that territory, try again", GetDefenderTerritory);
            }

            if (CurrentPlayer.OwnedTerritories.Contains(DefenderTerritory))
            {
                return new SelectTerritory("Sorry, you can't attack yourself", GetDefenderTerritory);
            }

            if (!AttackerTerritory.GetBorders().Contains(DefenderTerritory.GetName()))
            {
                return new SelectTerritory("Sorry, that territory does not border the attacking territory", GetDefenderTerritory);
            }

            return new SelectNumber("Input number to attack with (0 to stop attacking)", GetNumAttackers, 0, Math.Min(AttackerTerritory.GetTroops() - 1, 3));
        }

        /// Russell Phillips 2/20/2025
        /// <summary>
        /// Does an attack with a number of troops
        /// </summary>
        /// <param name="attackers">number of troops to attack with</param>
        /// <returns>UImanager</returns>
        public UIManager GetNumAttackers(int attackers)
        {
            if (attackers > AttackerTerritory.GetTroops() - 1 || attackers < 0 || attackers > 3) // have to leave one troop behind
            {
                return new SelectNumber("Sorry, invalid number of troops", GetNumAttackers, 0, Math.Min(AttackerTerritory.GetTroops() - 1, 3));
            }

            Battle(attackers);

            string output = $"You now have {AttackerTerritory.GetTroops()} troops left, and the defender has {DefenderTerritory.GetTroops()} troops left ";

            if (CheckBattleWon(gameState.CurrentTurnsPlayer))
            {
                return new SelectNumber("Input number of troops to transfer", TransferTroops, 1, AttackerTerritory.GetTroops() - 1);
            }

            if (AttackerTerritory.GetTroops() <= 1)
            {
                return new SelectTerritory("You no longer have enough troops to attack. Input another territory to attack from", GetAttackerTerritory);
            }

            if (attackers > 0)
            {
                return new SelectNumber("Input number to attack with (0 to stop attacking)", GetNumAttackers, 0, Math.Min(AttackerTerritory.GetTroops() - 1, 3));
            }

            return new SelectTerritory("Input another territory to attack from", GetAttackerTerritory);
        }

        /// Russell Phillips 2/20/2025
        /// <summary>
        /// Moves a number of troops after winning a battle
        /// </summary>
        /// <param name="transfer">number of troops to transfer</param>
        /// <returns>UIManager</returns>
        private UIManager TransferTroops(int transfer)
        {

            if (transfer > AttackerTerritory.GetTroops() - 1)
            {
                //return new UIManager("Sorry, but you can't transfer that many trops", TransferTroops);
            }

            if (transfer < 1)
            {
                //return new UIManager("Sorry, but you must transfer at least one troop", TransferTroops);
            }

            AttackerTerritory.SetTroops(AttackerTerritory.GetTroops() - transfer);
            DefenderTerritory.SetTroops(transfer);

            gameState.CheckPlayerLost();

            return new SelectTerritory("Input another territory to attack from", GetAttackerTerritory);
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Checks if the recent battle was won
        /// </summary>
        public bool CheckBattleWon(Player attackingPlayer)
        {
            if (DefenderTerritory.GetTroops() <= 0)
            {

                for (int i = 0; i < Players.Length; i++) // make sure no one else owns the territory
                {
                    if (Players[i].OwnedTerritories.Contains(DefenderTerritory))
                    {
                        Players[i].OwnedTerritories.Remove(DefenderTerritory);
                        break;
                    }
                }

                attackingPlayer.OwnedTerritories.Add(DefenderTerritory);
                DefenderTerritory.iconChange = true;

                BattleWon = true;
                return true;
            }
            return false;
        }

        /// Russell Phillips 4/28/2025
        /// <summary>
        /// Has the current player draw a card
        /// </summary>
        public void DrawCard()
        {
            gameState.CurrentTurnsPlayer.DrawCard();
        }
    }
}