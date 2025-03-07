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
        private Territory currTerritory;
        internal DraftPhase(Player currPlay, Board actBoar) : base(currPlay, actBoar)
        {
        }

        public override UIManager Start()
        {
            troopsRemaining = DraftableTroops();
            return new UIManager("You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
        }

        public UIManager ChooseTerritory(string inp)
        {
            currTerritory = GetTerritory(inp);
            if (currTerritory == null)
            {
                return new UIManager("Territory not found, please try again", ChooseTerritory);
            }

            return new UIManager("You have {troopsRemaining} troops remaining. Input number of troops", ChooseTerritory);
        }

        public UIManager ChooseNumTroops(string inp)
        {
            int numTroops = int.Parse(inp);
            if (numTroops <= troopsRemaining)
            {
                currTerritory.SetTroops(currTerritory.GetTroops() + numTroops);
            }
            troopsRemaining -= numTroops;

            if (troopsRemaining > 0 )
            {
                return new UIManager("You have {troopsRemaining} troops remaining. Choose a territory", ChooseTerritory);
            }
            else
            {
                return new UIManager();
            }
        }

        internal int DraftableTroops()
        {
            int numOwnedTerritories = currentPlayer.OwnedTerritories.Count;
            int draftableTroops = 0;
            if (numOwnedTerritories > 8)
            {
                draftableTroops = numOwnedTerritories / 3;
                foreach (Region r in this.activeBoard.allRegions){ 
                    if(r.AllTerritoriesOwnedByPlayer(currentPlayer)){ 
                        draftableTroops += r.GetRegionBonus(); }}
            }
            if(draftableTroops < 3) { draftableTroops = 3; }
            return draftableTroops;
        }
        internal int CardDraft()
        {
            int draftableTroops = 0;
            if(currentPlayer.GetNumCardsInHand() >= 3)
            {
                currentPlayer.TradeCards();
            }
            return draftableTroops;
        }
    }
}
