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

using SynchronousRisk.obj.Game_Pieces;

namespace SynchronousRisk
{
    /// Russell Phillips 4/3/2025
    /// <summary>
    /// Class that represents gamestate
    /// </summary>
    public class GameState
    {
        Deck Deck;

        public PlayableForm playableForm;

        public Board[] Boards;
        public int CurrentBoardIndex;
        public Player[] Players;
        public Player CurrentTurnsPlayer;
        public int currPlayer;

        public bool mapChange;

        public int PhaseInt;

        public int numPlayersPerBoard;

        Random Rand = new Random();

        public GameState(int numBoards, int numPlayers, Bitmap[] icons, PlayableForm form)
        {
            Deck = new Deck(new Board(this, Rand).GetTerritories());
            PhaseInt = 0;
            CurrentBoardIndex = 0;
            SetUpPlayers(numPlayers, icons);

            playableForm = form;
            
            Boards = new Board[numBoards];
            for (int i = 0; i < numBoards; i++)
            {
                Boards[i] = new Board(this, Rand);
            }

            DivideTerritories();

            GeneratePortals();

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

        private void GeneratePortals()
        {
            for (int i = 0; i < Boards.Count(); i++)
            {
                int TerritoryInd = Rand.Next(Boards[i].GetTerritories().Length);
                int NextBoard = (i + 1) % Boards.Count();
                Territory Current = Boards[i].GetTerritories()[TerritoryInd];
                Territory Next = Boards[NextBoard].GetTerritories()[TerritoryInd];
                Current.ExitPortal = new Portal(Current, Next);
                Next.ExitPortal = new Portal(Next, Current);
                Current.PortalPresent = true;
                Next.PortalPresent = true;
            }
        }

        /// Russell Phillips 3/18/2025
        /// <summary>
        /// Start a new turn for the next player
        /// </summary>
        public void NextPlayerTurn()
        {
            do
            {
                currPlayer = (currPlayer + 1) % Players.Count();
                CurrentTurnsPlayer = Players[currPlayer];
            } while (CurrentTurnsPlayer.OwnedTerritories.Count == 0);

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

        public void CheckPlayerLost()
        {
            List<Player> activePlayers = new List<Player>();
            foreach (Player player in Players)
            {
                if (player.active) activePlayers.Add(player);
                if (player.OwnedTerritories.Count() == 0 && player.active)
                {
                    player.active = false;

                    CurrentTurnsPlayer.GetHand().Add(player.GetHand());

                    if (CurrentTurnsPlayer.GetHand().CountCards() > 5)
                    {
                        Phase draft = new DraftPhase(this, false);
                        ExchangeCards ec = new ExchangeCards(playableForm, CurrentTurnsPlayer, true);
                        GetActiveBoard().Phases.AddFirst(draft);
                        playableForm.SelectNextScreen();
                        ec.Show();
                    }
                }
            }
            if (activePlayers.Count == 1) { /* Run the win conditional */}
        }
    }
}
