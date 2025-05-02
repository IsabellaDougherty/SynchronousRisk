using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynchronousRisk
{
    ///IAD 2/24/2025 <summary> Player class to represent a player in the game of Risk that will hold a hand of cards and a list of territories owned. </summary>
    public class Player
    {
        Hand playerHand = new Hand();
        Deck deck;
        public bool active { get; set; }
        private Bitmap Icon { get; set; }
        public List<Territory> OwnedTerritories { get; set; } = new List<Territory>();
        public Player(Deck d, Bitmap i) 
        {
            deck = d;
            Icon = i;
            active = true;
        }
        ///IAD 2/24/2025 <summary> Draws a card from the deck object provided within the parameter. </summary>
        public void DrawCard() { playerHand.Add(deck.Draw()); }
        /// IAD 2/24/2025 <summary> Draws a card from the deck object provided within the parameter. </summary> <param name="card"></param>
        public void DiscardCard(Card card) { deck.Discard(card); }
        /// IAD 2/24/2025 <summary> Returns the territories owned by the player. </summary>
        public Hand GetHand() { return playerHand; }
        /// IAD 2/24/2025 <summary> Returns the territories owned by the player. </summary>
        public Bitmap GetIcon() { return Icon; }
        /// IAD 2/24/2025 <summary> Returns the territories owned by the player. </summary>
        public int GetNumCardsInHand() { return playerHand.CountCards(); }
        ///IAD 2/24/2025 <summary> Trades in best possible set of cards and returns the value of troops the player should be alloted to place on the board. </summary>
        public int TradeCards()
        {
            Card[] tradeCards = playerHand.BestExchangeOption();
            foreach (Card card in tradeCards) { playerHand.Remove(card); }
            return playerHand.ExchangeValue(tradeCards);
        }
    }
}
