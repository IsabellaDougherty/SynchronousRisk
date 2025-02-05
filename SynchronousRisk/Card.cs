namespace SynchronousRisk
{

    enum CardType { Infantry, Cavalry, Artillery, Wild }

    class Card : Icomparable<Card>
    {
        private string territory { get; }

        private CardType type { get; }

        public Card(string territory, CardType type)
        {
            this.territory = territory;
            this.type = type;
        }

        public bool Compare(Card other)
        {
            if (other.Type == CardType.Wild || this.type == CardType.Wild)
            {
                return true;
            }

            return other.CardType == this.CardType;
        }

        public card Coerce(Card other)
        {
            if (this.type == CardType.Wild)
            {
                return other
            }

            return this
        }

    }
}