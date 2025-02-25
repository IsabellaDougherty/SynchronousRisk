using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{
    public class Phases
    {

        // IAD 2/24/2025
        /// <summary>
        /// Base class for all independent phases to inherit from for integratable functions that interact with the UI directly.
        /// </summary>
        internal Territory[] allTerritories;
        internal Player currentPlayer;
        internal Board activeBoard;
        public Phases(Territory[] allTerrs, Player currPlay, Board actBoar) 
        {
            allTerritories = allTerrs;
            currentPlayer = currPlay;
            activeBoard = actBoar;
        }
        public int GetUserInputNumber()
        {
            int numReturn;
            while (!int.TryParse(Console.ReadLine(), out numReturn))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
            return numReturn;
        }
        public Territory GetTerritory(string name)
        {
            Territory terr = null;
            foreach(Territory t in allTerritories) 
            { if (t.GetName() == name) { terr = t; break; }}
            if(terr == null) { Console.WriteLine("Territory not found."); return terr; }
            else return terr;
        }
    }
}
