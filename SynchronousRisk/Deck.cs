using System.Collections.Generic;

namespace SynchronousRisk
{

    class Deck
    {
        private Queue<Card> cards = new();

        private Queue<Card> discard = new();

        public Deck(List<string> territories)
        {
            List<Card> cards = new();
            cards.Add(Card("wild", CardType.Wild)); // two wilds in each deck
            cards.Add(Card("wild", CardType.Wild));

            for (int i = 0; i < territories.Length(); i++) // each card is an infantry, cavalry, or artillery, roughly evenly
            {
                switch (i % 3)
                {
                    case 0:
                        cards.Add(Card(territories[i], CardType.Infantry))
                        break;
                    case 1:
                        cards.Add(Card(territories[i], CardType.Cavalry))
                        break;
                    case 2:
                        cards.Add(Card(territories[i], CardType.Artillery))
                        break;
                }
            }

            Shuffle();
        }

        private void Shuffle()
        {
            Random.Shared.Shuffle(CollectionsMarshal.AsSpan(cards));

        }

        public Card Draw()
        {
            if (cards.Length() == 0) // if no more cards to draw, shuffle the discard into a new draw pile
            {
                cards = discard;
                discard = new();
                Shuffle();
            }
            return cards.Dequeue();
        }

        public void discard(Card card)
        {
            discard.Enqueue(card);
        }

    }
}