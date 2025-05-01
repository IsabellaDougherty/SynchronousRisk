using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{

    /// Russell Phillips 4/10/2025
    /// <summary>
    /// Phase that represents the setup part of risk, placing all troops given to each player one at a time
    /// </summary>
    internal class SetupPhase : Phase
    {
        Dictionary<Player, int> TroopsLeftPerPlayer;
        new List<Player> Players;

        int CurrentPlayerIndex = 0;

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Makes a new setup phase
        /// </summary>
        /// <param name="g">gameState</param>
        /// <param name="players">List of players on this board</param>
        public SetupPhase(GameState g, List<Player> players) : base(g)
        {
            Players = players;

            TroopsLeftPerPlayer = new Dictionary<Player, int>();
            int numTroops;
            int numPlayers = Players.Count();
            switch (numPlayers)
            {
                case 2:
                    numTroops = 40;
                    break;
                case 3:
                    numTroops = 25;
                    break;
                case 4:
                    numTroops = 30;
                    break;
                case 5:
                    numTroops = 25;
                    break;
                case 6:
                    numTroops = 20;
                    break;
                default:
                    numTroops = 20;
                    break;
            }

            foreach (Player player in Players)
            {
                TroopsLeftPerPlayer[player] = numTroops - player.OwnedTerritories.Count();
            }

            CanContinue = false;
            Players = players;
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Makes a setup phase with a forced number of troops to place
        /// </summary>
        /// <param name="g">gameState</param>
        /// <param name="players">List of players on this board</param>
        /// <param name="numTroops">number of troops to place</param>
        public SetupPhase(GameState g, List<Player> players, int numTroops) : base(g)
        {
            Players = players;
            TroopsLeftPerPlayer = new Dictionary<Player, int>();
            foreach (Player player in Players)
            {
                TroopsLeftPerPlayer[player] = numTroops;
            }

            CanContinue = false;
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Beginning UIManager
        /// </summary>
        /// <returns>UImanager</returns>
        public override UIManager Start()
        {
            return new SelectTerritory("Choose a territory to place a troop", Next);
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Place one troop and move on to the next player
        /// </summary>
        /// <param name="territory">Territory to add the troop</param>
        /// <returns>Next UImanager</returns>
        public UIManager Next(Territory territory)
        {
            if (!gameState.DoesPlayerOwnTerritory(gameState.GetCurrentTurnsPlayer(), territory))
            {
                return new SelectTerritory("Sorry you don't own that territory, try again", Next);
            }

            territory.SetTroops(territory.GetTroops() + 1);
            TroopsLeftPerPlayer[gameState.GetCurrentTurnsPlayer()] -= 1;

            if (CheckDone())
            {
                gameState.RestartTurns();
                return new UIManager();
            }

            FindNextPlayer();

            return new SelectTerritory("Choose a territory to place a troop", Next);
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Checks if all players have place all of their troops
        /// </summary>
        /// <returns></returns>
        private bool CheckDone()
        {
            foreach (Player player in Players)
            {
                if (TroopsLeftPerPlayer[player] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Finds the next player that needs to place a troop
        /// </summary>
        private void FindNextPlayer()
        {
            do
            {
                CurrentPlayerIndex = (CurrentPlayerIndex + 1) % Players.Count();
            }
            while (TroopsLeftPerPlayer[Players[CurrentPlayerIndex]] == 0);

            SetCurrentPlayer();
        }

        /// Russell Phillips 4/10/2025
        /// <summary>
        /// Sets the player who needs to place the next troop as the current turns player
        /// </summary>
        public void SetCurrentPlayer()
        {
            gameState.currPlayer = Array.IndexOf(gameState.Players, Players[CurrentPlayerIndex]);
        }
    }
}
