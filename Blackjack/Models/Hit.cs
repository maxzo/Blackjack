using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blackjack.BlackjackServiceReference;

namespace Blackjack.Models
{
    public class Hit
    {
        public Card DealerCard { get; set; }
        public IEnumerable<Card> YourCards { get; set; }
        public IEnumerable<int> DealerValues { get; set; }
        public IEnumerable<int> YourValues { get; set; }
    }
}