using Blackjack.UI.BlackjackServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Blackjack.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var service = new BlackjackServiceClient();
            var startingHand = service.StartGame(1);

            return View(startingHand);
        }

        public ActionResult GiveCardToDealer()
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var card = service.GiveCardToDealer();

                return Json(new { Face = card.Face.ToString(), Suit = card.Suit.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GiveCardToPlayer(int playerIndex)
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var card = service.GiveCardToPlayer(playerIndex);

                return Json(new { Face = card.Face.ToString(), Suit = card.Suit.ToString() }, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult PlayerHasBlackjack(int playerIndex)
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var result = service.PlayerHasBlackjack(playerIndex);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult PlayerLost(int playerIndex)
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var result = service.PlayerLost(playerIndex);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DealerStand()
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var result = service.DealerStand();

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult GiveAllCardsToDealer()
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                IList<Card> cards = new List<Card>(0);

                while (!service.DealerStand())
                {
                    cards.Add(service.GiveCardToDealer());
                }

                return Json((from c in cards
                             select new { Face = c.Face.ToString(), Suit = c.Suit.ToString() }).ToArray(),
                    JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DealerHasBlackjack()
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var result = service.DealerHasBlackjack();

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }

        public ActionResult PlayerWins(int playerIndex)
        {
            if (Request.IsAjaxRequest())
            {
                var service = new BlackjackServiceClient();
                var result = service.PlayerWins(playerIndex);

                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return RedirectToAction("Index");
        }
    }
}
