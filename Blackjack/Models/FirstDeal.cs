using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Blackjack.BlackjackServiceReference;

namespace Blackjack.Models
{
    public class FirstDeal
    {
        public Card DealerCard { get; set; }
        public Card YourCard1 { get; set; }
        public Card YourCard2 { get; set; }
    }
}