﻿using System;
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
        internal Player currentPlayer;
        internal Board activeBoard;
        internal Player[] Players;
        public Phases(Player currPlay, Board actBoar, Player[] players) 
        {
            currentPlayer = currPlay;
            activeBoard = actBoar;
            Players = players;
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

        public virtual UIManager Start()
        {
            return null;
        }
    }
}
