using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk.PhaseProcessing
{
    //IAD 2/24/2025
    /// <summary>
    /// Draft phase class to handle the drafting of troops for the current player and deployment of troops to the board.
    /// </summary>
    internal class DraftPhase : Phases
    {
        internal DraftPhase(Territory[] allTerrs, Player currPlay, Board actBoar) : base(allTerrs, currPlay, actBoar)
        {
            this.allTerritories = allTerrs;
            this.currentPlayer = currPlay;
            this.activeBoard = actBoar;
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
