using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Contracts.Entities
{
    [DataContract]
    public class StartingHands
    {
        [DataMember]
        public IEnumerable<Card> DealerHand { get; set; }

        [DataMember]
        public IEnumerable<IEnumerable<Card>> PlayerHands { get; set; }

        public StartingHands(int players)
        {
            this.DealerHand = new List<Card>();
            this.PlayerHands = new List<List<Card>>(0);

            for (int i = 0; i < players; i++)
            {
                ((List<List<Card>>)(this.PlayerHands)).Add(new List<Card>());
            }
        }
    }
}
