
using System;

namespace SynchronousRisk
{

    public enum CardType { Infantry, Cavalry, Artillery, Wild }

    /* Russell Phillips
	   2/04/2025
	   Card class with a territory an card type */
    public class Card
    {
        public Territory Territory { get; }

        public CardType Type { get; }
        public String Name { get; }

        public Card(String territory, CardType type)
        {
            this.Name = territory;
            this.Type = type;
        }

        public Card(Territory territory, CardType type)
        {
            //IAD 2/17/2025
            /// <summary>
            /// Pulls the name using the `GetName()` method from the Territory class to create a new Card object.
            /// Note: You can create a card with a String alone by inputting a String directly here in place of a Territory object.
            /// </summary>
            this.Name = territory.GetName();
            this.Type = type;
        }

        // Returns if cards could be considered the same type, including wilds
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