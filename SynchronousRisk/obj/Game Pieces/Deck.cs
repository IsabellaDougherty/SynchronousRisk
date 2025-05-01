using System;
using System.Collections.Generic;

namespace SynchronousRisk
{
    /* Russell Phillilps
	   2/04/2025
	   Deck class, makes a deck of cards and manages drawing and discarding */
    public class Deck
    {
        private Queue<Card> Cards = new Queue<Card>();

        private Queue<Card> DiscardPile = new Queue<Card>();

        private Random Rand = new Random();

        public Deck(Territory[] territories)
        {


            List<Card> cards = new List<Card>();
            cards.Add(new Card("Wild", CardType.Wild)); // Two wilds in each deck
            cards.Add(new Card("Wild", CardType.Wild));

            for (int i = 0; i < territories.Length; i++) // Each card is an infantry, cavalry, or artillery, roughly evenly
            {
                switch (i % 3)
                {
                    case 0:
                        cards.Add(new Card(territories[i], CardType.Infantry));
                        break;
                    case 1:
                        cards.Add(new Card(territories[i], CardType.Cavalry));
                        break;
                    case 2:
                        cards.Add(new Card(territories[i], CardType.Artillery));
                        break;
                }
            }

            Cards = new Queue<Card>(cards);
            Shuffle();
        }

        /// Russell Phillips 2/04/2025
        /// <summary>
        /// Shuffle the draw pile (using the Fisher-Yates shuffle)
        /// </summary>
        /// https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle
        private void Shuffle()
        {
            List<Card> lst = new List<Card>(Cards);
            for (int i = Cards.Count - 1; i > 0; i--)
            {
                int j = Rand.Next(i + 1);
                Card value = lst[j];
                lst[j] = lst[i];
                lst[i] = value;
            }
            Cards = new Queue<Card>(lst);
        }

        /// Russell Phillips 2/04/2025
        /// <summary>
        /// Draw card from deck
        /// </summary>
        /// <returns></returns>
        public Card Draw()
        {
            if (Cards.Count == 0)
            {
                Cards = DiscardPile;
                DiscardPile = new Queue<Card>();
                Shuffle();
            }
            return Cards.Dequeue();
        }

        /// Russell Phillips 2/04/2025
        /// <summary>
        /// Add a discarded card to the discard pile
        /// </summary>
        /// <param name="card">card to discard</param>
        public void Discard(Card card)
        {
            DiscardPile.Enqueue(card);
        }

    }
}