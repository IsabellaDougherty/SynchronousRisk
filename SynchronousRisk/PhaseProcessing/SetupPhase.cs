using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{
    internal class SetupPhase : Phases
    {
        Dictionary<Player, int> TroopsLeftPerPlayer;
        public SetupPhase(GameState g) : base(g)
        {

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

            foreach (Player player in gameState.GetPlayers())
            {
                TroopsLeftPerPlayer[player] = numTroops - player.OwnedTerritories.Count();
            }

            CanContinue = false;
        }

        public SetupPhase(GameState g, int numTroops) : base(g)
        {
            TroopsLeftPerPlayer = new Dictionary<Player, int>();
            foreach (Player player in gameState.GetPlayers())
            {
                TroopsLeftPerPlayer[player] = numTroops;
            }

            CanContinue = false;
        }

        public override UIManager Start()
        {
            return new SelectTerritory("Choose a territory to place a troop", Next);
        }

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

        private bool CheckDone()
        {
            foreach (Player player in gameState.GetPlayers())
            {
                if (TroopsLeftPerPlayer[player] != 0)
                {
                    return false;
                }
            }

            return true;
        }

        private void FindNextPlayer()
        {
            do
            {
                gameState.NextPlayerTurn();
            }
            while (TroopsLeftPerPlayer[gameState.GetCurrentTurnsPlayer()] == 0);

        }
    }
}
