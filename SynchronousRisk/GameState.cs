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

        private LinkedList<Phases> Phases;

        public GameState(int numBoards, int numPlayers, Bitmap[] icons)
        {
            Boards = new Board[numBoards];
            for (int i = 0; i < numBoards; i++)
            {
                Boards[i] = new Board();
            }
            Deck = new Deck(Boards[1].GetTerritories());
            PhaseInt = 0;
            CurrentBoardIndex = 0;
            SetUpPlayers(numPlayers, icons);
            DivideTerritories();

            Phases = new LinkedList<Phases>();
            Phases.AddLast(new SetupPhase(this)); //dummy phase as NextPhase removes a phase before selecting the next one
            Phases.AddLast(new SetupPhase(this, 1));

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
            numPlayersPerBoard = Players.Count() / Boards.Count();

            for (int Bidx = 0; Bidx < Boards.Length; Bidx++)
            {
                List<Territory> territories = new List<Territory>(Boards[Bidx].GetTerritories());
                shuffle(territories);
                int offset = Bidx * numPlayersPerBoard;

                for (int i = 0; i < territories.Count; i++)
                {
                    Players[offset + (i % numPlayersPerBoard)].OwnedTerritories.Add(territories[i]);
                    territories[i].SetTroops(1);
                }

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

        public UIManager NextPhase()
        {
            Phases.RemoveFirst();

            if (Phases.Count() == 0)
            {
                NextPlayerTurn();
                Phases.AddLast(new DraftPhase(this));
                Phases.AddLast(new AttackPhase(this));
                Phases.AddLast(new FortifyPhase(this));
            }

            return Phases.First.Value.Start();
        }

        public Phases GetCurrentPhase()
        {
            return Phases.First.Value;
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

        public void SetActiveBoard(int nextBoard)
        {
            CurrentBoardIndex = nextBoard;
        }
    }
}
