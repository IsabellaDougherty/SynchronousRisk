using SynchronousRisk.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SynchronousRisk.PhaseProcessing
{
    //IAD 2/24/2025
    /// <summary>
    /// Draft phase class to handle the drafting of troops for the current player and deployment of troops to the board.
    /// </summary>
    internal class DraftPhase : Phases
    {
        private int troopsRemaining;
        private Territory CurrTerritory;
        internal DraftPhase(GameState g) : base(g)
        {
        }

        public override UIManager Start()
        {
            troopsRemaining = DraftableTroops();
            return new SelectTerritory($"You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
        }

        public UIManager ChooseTerritory(Territory currTerritory)
        {
            CurrTerritory = currTerritory;
            if (currTerritory == null)
            {
                return new SelectTerritory("Territory not found, please try again", ChooseTerritory);
            }

            if (!CurrentPlayer.OwnedTerritories.Contains(currTerritory))
            {
                return new SelectTerritory("You don't own this territory, please try again", ChooseTerritory);
            }

            return new SelectNumber($"You have {troopsRemaining} troops remaining. Input number of troops", ChooseNumTroops, 0, troopsRemaining);
        }

        public UIManager ChooseNumTroops(int numTroops)
        {
            if (numTroops <= troopsRemaining)
            {
                CurrTerritory.SetTroops(CurrTerritory.GetTroops() + numTroops);
            }
            troopsRemaining -= numTroops;

            if (troopsRemaining > 0 )
            {
                return new SelectTerritory($"You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
            }
            else
            {
                return new AttackPhase(gameState).Start();
            }
        }

        internal int DraftableTroops()
        {
            int numOwnedTerritories = CurrentPlayer.OwnedTerritories.Count;
            int draftableTroops = 0;
            if (numOwnedTerritories > 8)
            {
                draftableTroops = numOwnedTerritories / 3;
                foreach (Region r in this.activeBoard.GetRegions()){ 
                    if(r.AllTerritoriesOwnedByPlayer(CurrentPlayer)){ 
                        draftableTroops += r.GetRegionBonus(); }}
            }
            if(draftableTroops < 3) { draftableTroops = 3; }
            return draftableTroops;
        }
        internal int CardDraft()
        {
            int draftableTroops = 0;
            if(CurrentPlayer.GetNumCardsInHand() >= 3)
            {
                CurrentPlayer.TradeCards();
            }
            return draftableTroops;
        }
    }
}
