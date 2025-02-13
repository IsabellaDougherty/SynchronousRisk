
namespace SynchronousRisk
{

    enum CardType { Infantry, Cavalry, Artillery, Wild }

    /* Russell Phillips
	   2/04/2025
	   Card class with a territory an card type */
    class Card
    {
        public string Territory { get; }

        public CardType Type { get; }

        public Card(string territory, CardType type)
        {
            this.Territory = territory;
            this.Type = type;
        }

        // returns if cards could be considered the same type, including wilds
        public bool Compare(Card other)
        {
            if (other.Type == CardType.Wild || this.Type == CardType.Wild)
            {
                return true;
            }

            return other.Type == this.Type;
        }

        // Returns the type of non-wild card
        public Card Coerce(Card other)
        {
            if (this.Type == CardType.Wild)
            {
                return other;
            }

            return this;
        }

    }
}