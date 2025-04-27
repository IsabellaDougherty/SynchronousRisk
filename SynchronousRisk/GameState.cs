using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SynchronousRisk.Menus;
using SynchronousRisk.PhaseProcessing;
using SynchronousRisk;
using System.Drawing;
using System.Security.Policy;
using System.Windows.Forms;

namespace SynchronousRisk
{
    /// Russell Phillips 4/3/2025
    /// <summary>
    /// Class that represents gamestate
    /// </summary>
    public class GameState
    {
        Deck Deck;

        public Board[] Boards;
        public int CurrentBoardIndex;
        public Player[] Players;
        public Player CurrentTurnsPlayer;
        public int currPlayer;

        public bool mapChange;

        public int PhaseInt;

        public int numPlayersPerBoard;

        public GameState(int numBoards, int numPlayers, Bitmap[] icons)
        {
            Deck = new Deck(new Board(this).GetTerritories());
            PhaseInt = 0;
            CurrentBoardIndex = 0;
            SetUpPlayers(numPlayers, icons);
            
            Boards = new Board[numBoards];
            for (int i = 0; i < numBoards; i++)
            {
                Boards[i] = new Board(this);
            }

            DivideTerritories();

            mapChange = false;
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Set up a list of players and an index that represents whose turn it is
        /// </summary>
        public void SetUpPlayers(int numPlayers, Bitmap[] icons)
        {
            Players = new Player[numPlayers];      //*Boards.Count()];
            for (int i = 0; i < Players.Length; i++)
            {
                Players[i] = new Player(Deck, icons[i]);
            }

            currPlayer = 0;
            CurrentTurnsPlayer = Players[0];

        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// randomly distrubutes territories from each board to corresponding players
        /// </summary>
        private void DivideTerritories()
        {
            List<Player>[] distribution = new List<Player>[Boards.Count()];

            for (int i = 0; i < Boards.Count(); i++)
            {
                distribution[i] = new List<Player>();
            }

            for (int i = 0; i < Players.Count(); i++)
            {
                distribution[i % distribution.Length].Add(Players[i]);
            }

            for(int i  = 0; i < Boards.Count(); i++)
            {
                Boards[i].DistributeTerritories(distribution[i], this);
            }
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Start a new turn for the next player
        /// </summary>
        public void NextPlayerTurn()
        {
            currPlayer = (currPlayer + 1) % Players.Count();
            CurrentTurnsPlayer = Players[currPlayer];

            foreach (Board board in Boards)
            {
                board.CreatePhases(this);
            }
        }

        public bool CanEndTurn()
        {
            foreach (Board board in Boards)
            {
                if (!board.CanEndTurn()) 
                    return false;
            }
            return true;
        }

        public void RestartTurns()
        {
            currPlayer = 0;
            CurrentTurnsPlayer = Players[currPlayer];

        }

        public Phase GetCurrentPhase()
        {
            return GetActiveBoard().GetCurrentPhase();
        }

        public void NextPhase()
        {
            GetActiveBoard().NextPhase();
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
                    if (terr == owned) return p;
            return null;
        }

        public Player[] GetPlayers()
        {
            return Players;
        }

        public Board GetActiveBoard()
        {
            return Boards[CurrentBoardIndex];
        }

        public UIManager SetActiveBoard(int nextBoard)
        {
            CurrentBoardIndex = nextBoard;

            if (GetActiveBoard().GetCurrentPhase() is SetupPhase sp && GetActiveBoard().Phases.Count() != 0)
            {
                sp.SetCurrentPlayer();
            }

            return GetActiveBoard().CurrMenu;
        }
    }
}
