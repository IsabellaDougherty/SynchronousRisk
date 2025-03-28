﻿using SynchronousRisk.Menus;
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
        internal DraftPhase(Player currPlay, Board actBoar, Player[] players) : base(currPlay, actBoar, players)
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

            if (!currentPlayer.OwnedTerritories.Contains(currTerritory))
            {
                return new SelectTerritory("You don't own this territory, please try again", ChooseTerritory);
            }

            return new UIManager($"You have {troopsRemaining} troops remaining. Input number of troops", ChooseNumTroops);
        }

        public UIManager ChooseNumTroops(string inp)
        {
            int numTroops = int.Parse(inp);
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
                return new AttackPhase(currentPlayer, activeBoard, Players).Start();
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
