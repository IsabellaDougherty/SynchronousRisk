using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk.Menus;

namespace SynchronousRisk.PhaseProcessing
{
    public class Phases
    {

        // IAD 2/24/2025
        /// <summary>
        /// Base class for all independent phases to inherit from for integratable functions that interact with the UI directly.
        /// </summary>
        internal GameState gameState;
        internal Player CurrentPlayer { get { return gameState.CurrentTurnsPlayer; } }
        internal Board activeBoard { get { return gameState.Board; } }
        internal Player[] Players { get { return gameState.Players; } }
        public Phases(GameState g) 
        {
            gameState = g;
        }
        public int GetUserInputNumber(int min, int max)
        {
            int numReturn;
            while (!int.TryParse(Console.ReadLine(), out numReturn) && numReturn >= min && numReturn <= max)
            {
                Console.WriteLine($"Invalid input. Please enter a valid number, between {min} and {max}");
            }
            return numReturn;
        }
        public int GetUserInputNumber()
        {
            int numReturn;
            while (!int.TryParse(Console.ReadLine(), out numReturn))
            {
                Console.WriteLine($"Invalid input. Please enter a valid number");
            }
            return numReturn;
        }

        public Territory GetUserInputTerritory()
        {
            Territory territoryReturn = GetTerritory(Console.ReadLine());
            return territoryReturn;
        }
        public Territory GetTerritory(string name)
        {
            Territory terr = null;
            foreach(Territory t in activeBoard.GetTerritories()) 
            { if (t.GetName() == name) { terr = t; break; }}
            if(terr == null) { Console.WriteLine("Territory not found."); return terr; }
            else return terr;
        }
        /// IAD 4/23/2025 <summary> Checks if a player has won the game. A player is considered to have won if they own all territories in the game. </summary>
        /// <param name="p"></param>
        public Player FindWinner()
        {
            foreach (Player pl in Players) 
                if (pl.active && activeBoard.GetTerritories().Count() == pl.OwnedTerritories.Count) 
                    return pl;
            return null;
        }
        /// IAD 4/23/2025 <summary> Checks if a player is active in the game. A player is considered active if they own at least one territory. </summary>
        /// <param name="p"></param>
        public bool PlayerActive(Player p)
        {
            if (p.OwnedTerritories.Count > 0) return true;
            else 
            { 
                p.active = false;
                return false; 
            }
        }

        public virtual UIManager Start()
        {
            return null;
        }
    }
}
