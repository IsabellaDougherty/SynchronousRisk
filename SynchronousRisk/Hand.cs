using System.Collections.Generic;

namespace SynchronousRisk
{

    class Hand
    {
        private List<Card> cards = new();

        public Hand() { }

        public Add(Card card)
        {
            cards.Add(card);
        }

        public List<Card> BestExchangeOption()
        {
            int maxValue = 0;
            List<Card> bestExchangeOption = new();
            for (int i = 0; i < cards.count; i++)
                for (int j = i + 1; j < cards.count; j++)
                    for (int k = j + 1; k < cards.count; k++)
                    {
                        List<Card> currentExchangeOption = { cards[i], cards[j], cards[k] };
                        if (ExchangeValue(currentExchangeOption) > maxValue)
                        {
                            maxValue = ExchangeValue(currentExchangeOption);
                            bestExchangeOption = currentExchangeOption;
                        }
                    }

            return bestExchangeOption;
        }

        public int ExchangeValue(List<Card> cards)
        {
            if (cards[0].Type != cards[1].Type && cards[1].Type != cards[2].Type && cards[0].Type != cards[1].type) // all different cards
            {
                return 10;
            }
            if (cards[0].Compare(cards[1]) && cards[1].Compare(cards[2])) // all same cards
            {
                switch (cards[0].Coerce(cards[1]).Coerce(cards[2]))
                {
                    case CardType.Infantry:
                        return 4;
                    case CardType.Cavalry:
                        return 6;
                    case CardType.Artillery:
                        return 8;
                    defaulf:
                        return 0;
                }
            }

            return 0;
        }
    }
}