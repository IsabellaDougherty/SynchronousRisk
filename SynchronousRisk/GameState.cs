using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk;
using System.Drawing;

namespace SynchronousRisk
{
    /// Russell Phillips 4/3/2025
    /// <summary>
    /// Class that represents gamestate
    /// </summary>
    public class GameState
    {
        Deck Deck;

        public Board Board;
        public Player[] Players;
        public Player CurrentTurnsPlayer;
        public int currPlayer;

        public int PhaseInt;

        public GameState()
        {
            Board = new Board();
            Deck = new Deck(Board.GetTerritories());
            PhaseInt = 0;
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Set up a list of players and an index that represents whose turn it is
        /// </summary>
        public void SetUpPlayers(int numPlayers, Bitmap[] icons)
        {
            Players = new Player[numPlayers];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new Player(Deck, icons[i]);
            }

            currPlayer = 0;
            CurrentTurnsPlayer = Players[0];

            DivideTerritories();
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Randomly divedes territories to each player as evenly as possible
        /// </summary>
        private void DivideTerritories()
        {
            List<Territory> territories = new List<Territory>(Board.GetTerritories());
            shuffle(territories);

            for (int i = 0; i < territories.Count; i++)
            {
                Players[i % Players.Count()].OwnedTerritories.Add(territories[i]);
                territories[i].SetTroops(1);
            }

        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// shuffles a generic list in place
        /// </summary>
        /// <typeparam name="T">type of list to be shuffled</typeparam>
        /// <param name="lst">list to be shuffled</param>
        /// <returns>shuffled list</returns>
        private List<T> shuffle<T>(List<T> lst)
        {
            Random Rand = new Random();
            for (int i = lst.Count - 1; i > 0; i--)
            {
                int j = Rand.Next(i + 1);
                T value = lst[j];
                lst[j] = lst[i];
                lst[i] = value;
            }
            return lst;
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Start a new turn for the next player
        /// </summary>
        public void NextPlayerTurn()
        {
            currPlayer = (currPlayer + 1) % Players.Count();
            CurrentTurnsPlayer = Players[currPlayer];
        }

        public void RestartTurns()
        {
            currPlayer = 0;
            CurrentTurnsPlayer = Players[currPlayer];

        }

        public Player GetCurrentTurnsPlayer()
        {
            return Players[currPlayer];
        }

        public bool DoesPlayerOwnTerritory(Player player, Territory territory)
        {
            return player.OwnedTerritories.Contains(territory);
        }
        public Player TerritoryOwnedByWho(Territory terr)
        {
            foreach (Player p in Players)
                foreach (Territory owned in p.OwnedTerritories)
                    if (owned.rgb.SequenceEqual(terr.rgb)) return p;
            return null;
        }

        public Player[] GetPlayers()
        {
            return Players;
        }

    }
}
