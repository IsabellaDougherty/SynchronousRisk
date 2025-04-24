using System.Collections.Generic;

namespace SynchronousRisk
{
    /* Russell Phillips
	   2/04/2025
	   Manages a hand of cards for a player, keeping track of current cards and exchange options */
    public class Hand
    {
        private List<Card> cards = new List<Card>();

        public Hand() { }

        public void Add(Card card)
        { cards.Add(card); }

        //IAD 2/24/2025: Implimented a remove method to remove a card from the hand.
        public void Remove(Card card) { cards.Remove(card); }
        public int CountCards() { return cards.Count; }
        /// IAD 4/24/2025 <summary> Returns the list of cards in hand. </summary>
        public List<Card> GetCards() { return cards; }

        /// <summary>
        /// Returns the best exchange option for cards currently in hand. If less than three cards are in hand, returns an array of null.
        /// </summary>
        /// <returns></returns>
        public Card[] BestExchangeOption()
        {
            //IAD 3/3/2025: Adjusted logic of function to return an array instead of a list to match the exchange value function
            //  and adjusted the bestExchangeOption to be a single value array of null fail checking.
            Card[] bestExchangeOption = { null };
            if (cards.Count < 3) { return bestExchangeOption; }
            int maxValue = 0;
            for (int i = 0; i < cards.Count; i++)
                for (int j = i + 1; j < cards.Count; j++)
                    for (int k = j + 1; k < cards.Count; k++)
                    {
                        Card[] currentExchangeOption = { cards[i], cards[j], cards[k] };
                        if (ExchangeValue(currentExchangeOption) > maxValue)
                        {
                            maxValue = ExchangeValue(currentExchangeOption);
                            bestExchangeOption = currentExchangeOption;
                        }
                    }
            return bestExchangeOption;
        }

        // Returns what an array of three cards would be worth if turned in
        public int ExchangeValue(Card[] cards)
        {
            if (cards[0].Type != cards[1].Type && cards[1].Type != cards[2].Type && cards[0].Type != cards[1].Type) // All different cards
            {
                return 10;
            }
            if (cards[0].Compare(cards[1]) && cards[1].Compare(cards[2])) // All same cards
            {
                switch (cards[0].Coerce(cards[1]).Coerce(cards[2]).Type)
                {
                    case CardType.Infantry:
                        return 4;
                    case CardType.Cavalry:
                        return 6;
                    case CardType.Artillery:
                        return 8;
                    default:
                        return 0;
                }
            }
            return 0;
        }
    }
}