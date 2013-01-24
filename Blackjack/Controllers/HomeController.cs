using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Blackjack.BlackjackServiceReference;
using Blackjack.Models;

namespace Blackjack.Controllers
{
    public class HomeController : Controller
    {
        private static IEnumerable<Card> deck;
        private static IList<int> dealerCardsIndexes;
        private static IList<int> yourCardsIndexes;

        //
        // GET: /Home/

        public ActionResult Index()
        {
            dealerCardsIndexes = new List<int>() { 0 };
            yourCardsIndexes = new List<int>() { 1, 2 };

            BlackjackServiceClient service = new BlackjackServiceClient();

            deck = service.GetShuffledDeck();

            FirstDeal firstDeal = new FirstDeal()
            {
                DealerCard = deck.ElementAt(0),
                YourCard1 = deck.ElementAt(1),
                YourCard2 = deck.ElementAt(2)
            };

            return View(firstDeal);
        }

        //
        // POST: /Home/Index/

        [HttpPost]
        public ActionResult Index(bool IsHit/*, string DealerCardFace, string DealerCardSuit,
            string YourCard1Face, string YourCard1Suit, string YourCard2Face, string YourCard2Suit*/)
        {
            if (IsHit)
            {
                yourCardsIndexes.Add(dealerCardsIndexes.Count + yourCardsIndexes.Count);

                return RedirectToAction("Hit");
            }
            else
            {
                return RedirectToAction("Stand");
            }
        }

        //
        // GET: /Home/Hit/

        public ActionResult Hit()
        {
            BlackjackServiceClient service = new BlackjackServiceClient();

            Hit hit = new Hit()
            {
                DealerCard = deck.ElementAt((from i in dealerCardsIndexes
                                             select i).First()),
                YourCards = from i in yourCardsIndexes
                            select deck.ElementAt(i),
                DealerValues = service.GetPossibleValues((from i in dealerCardsIndexes
                                                          select deck.ElementAt(i)).ToArray()),
                YourValues = service.GetPossibleValues((from i in yourCardsIndexes
                                                       select deck.ElementAt(i)).ToArray())
            };

            return View(hit);
        }

        //
        // POST: /Home/Hit/

        [HttpPost]
        public ActionResult Hit(bool IsHit)
        {
            if (IsHit)
            {
                yourCardsIndexes.Add(dealerCardsIndexes.Count + yourCardsIndexes.Count);

                return RedirectToAction("Hit");
            }
            else
            {
                return RedirectToAction("Stand");
            }
        }
    }
}
