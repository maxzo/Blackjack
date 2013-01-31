using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Contracts.Entities
{
    public enum CardFace
    {
        Ace,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    };
    public enum CardSuit
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    };

    [DataContract]
    public class Card
    {
        [DataMember]
        public CardFace Face { get; set; }

        [DataMember]
        public CardSuit Suit { get; set; }
    }
}
