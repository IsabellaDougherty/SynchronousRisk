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
    internal class DraftPhase : Phase
    {
        private int troopsRemaining;
        int exchangeTroops;
        private Territory CurrTerritory;
        internal DraftPhase(GameState g) : base(g)
        {
            CanContinue = false;
            troopsRemaining = DraftableTroops();
        }

        public DraftPhase(GameState g, bool f) : base(g)
        {
            CanContinue = false;
            troopsRemaining = 0;
        }

        /// Russell Phillips <summary>
        /// 
        /// </summary>
        public override UIManager Start()
        {
            gameState.PhaseInt = 1;
            gameState.phaseChange = true;
            if (CurrentPlayer.GetNumCardsInHand() > 5)
            {
                exchangeTroops = ForceTrade();
                troopsRemaining = DraftableTroops() + exchangeTroops;
            }
            else troopsRemaining = DraftableTroops();
            return new SelectTerritory($"You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
        }

        /// IAD and Russell Phillips 4/24/2025 <summary> Used by ExchangeCards for distributing new troops from cards being exchanged. </summary>
        /// <param name="exchangeTroops"></param>
        public override UIManager Start(int exchangeTroops)
        {
            gameState.PhaseInt = 1;
            troopsRemaining = exchangeTroops;
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
                return new UIManager();
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

        private int ForceTrade()
        {
            if (CurrentPlayer.GetNumCardsInHand() > 5)
            {
                exchangeTroops = CurrentPlayer.TradeCards();
            }
            return exchangeTroops;
        }

        //Reassign result of CardDraft to the currMenu in PlayableForm
        internal UIManager CardDraft()
        {
            if(CurrentPlayer.GetNumCardsInHand() >= 3)
                troopsRemaining += CurrentPlayer.TradeCards();
            return new SelectTerritory($"You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
        }
    }
}
