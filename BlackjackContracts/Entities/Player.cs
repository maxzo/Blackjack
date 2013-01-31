using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Contracts.Entities
{
    public enum PlayerType
    {
        Dealer,
        Normal
    }

    [DataContract]
    public class Player
    {
        [DataMember]
        public PlayerType Type { get; set; }

        [DataMember]
        public IEnumerable<Card> Hand { get; set; }

        public Player()
        {
            Hand = new List<Card>();
        }
    }
}
