using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk.Menus;

namespace SynchronousRisk.PhaseProcessing
{
    public class Phase
    {

        // IAD 2/24/2025
        /// <summary>
        /// Base class for all independent phases to inherit from for integratable functions that interact with the UI directly.
        /// </summary>
        internal GameState gameState;
        internal Player CurrentPlayer { get { return gameState.CurrentTurnsPlayer; } }
        internal Board activeBoard { get { return gameState.GetActiveBoard(); } }
        internal Player[] Players { get { return gameState.Players; } }

        internal bool CanContinue;

        public Phase(GameState g) 
        {
            gameState = g;
            CanContinue = false;
        }

        public Phase()
        {
            CanContinue = false;
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

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Virtual method to get the initial UIManager of a phase
        /// </summary>
        /// <returns>UIManager</returns>
        public virtual UIManager Start()
        {
            return null;
        }

        /// Russell Phillips 3/4/2025
        /// <summary>
        /// Virtual method to get the initial UIManager of a phase when it needs an integer input
        /// </summary>
        /// <returns>UIManager</returns>
        public virtual UIManager Start(int i)
        {
            return null;
        }
    }
}
