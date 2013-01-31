using Blackjack.Contracts;
using Blackjack.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blackjack.Services
{
    public class BlackjackService : IBlackjackService
    {
        private static Game game;

        public StartingHands StartGame(int players)
        {
            game = new Game(players);
            game.GiveCardToDealer();

            for (int i = 0; i < game.Players.Count(); i++)
            {
                game.GiveCardToPlayer(i);
                game.GiveCardToPlayer(i);
            }

            var startingHand = new StartingHands(0)
            {
                DealerHand = game.Dealer.Hand
            };

            for (int i = 0; i < game.Players.Count(); i++)
            {
                ((List<List<Card>>)(startingHand.PlayerHands)).Add(game.Players.ElementAt(i).Hand.ToList());
            }

            return startingHand;
        }

        public Card GiveCardToPlayer(int playerIndex)
        {
            return game.GiveCardToPlayer(playerIndex);
        }

        public Card GiveCardToDealer()
        {
            return game.GiveCardToDealer();
        }

        public IEnumerable<int> GetDealerPossibleValues()
        {
            return this.GetPossibleValues(game.Dealer.Hand);
        }

        public IEnumerable<int> GetPlayerPossibleValues(int playerIndex)
        {
            return this.GetPossibleValues(game.Players.ElementAt(playerIndex).Hand);
        }

        private IEnumerable<int> GetPossibleValues(IEnumerable<Card> Hand)
        {
            IList<int> values = new List<int>();

            foreach (var card in Hand)
            {
                switch (card.Face)
                {
                    case CardFace.Ace:
                        if (values.Count == 0)
                        {
                            values.Add(11);
                            values.Add(1);
                        }
                        else
                        {
                            int tamanho = values.Count;
                            for (int i = 0; i < tamanho; i++)
                            {
                                values[i] += 1;
                                values.Add(values[i] + 10);
                            }
                        }
                        break;
                    case CardFace.Two:
                    case CardFace.Three:
                    case CardFace.Four:
                    case CardFace.Five:
                    case CardFace.Six:
                    case CardFace.Seven:
                    case CardFace.Eight:
                    case CardFace.Nine:
                        if (values.Count == 0)
                        {
                            values.Add((int)(card.Face) + 1);
                        }
                        else
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                values[i] += (int)(card.Face) + 1;
                            }
                        }
                        break;
                    case CardFace.Ten:
                    case CardFace.Jack:
                    case CardFace.Queen:
                    case CardFace.King:
                        if (values.Count == 0)
                        {
                            values.Add(10);
                        }
                        else
                        {
                            for (int i = 0; i < values.Count; i++)
                            {
                                values[i] += 10;
                            }
                        }
                        break;
                }
            }

            return values.Distinct();
        }

        public bool PlayerHasBlackjack(int playerIndex)
        {
            return (game.Players.ElementAt(playerIndex).Hand.Count() == 2
                && this.GetPlayerPossibleValues(playerIndex).Contains(21));
        }

        public bool DealerHasBlackjack()
        {
            return (game.Dealer.Hand.Count() == 2
                && this.GetDealerPossibleValues().Contains(21));
        }

        public bool PlayerLost(int playerIndex)
        {
            return this.GetPlayerPossibleValues(playerIndex).All(x => x > 21);
        }

        public bool DealerStand()
        {
            return this.GetDealerPossibleValues().Max() > 16;
        }

        public int PlayerWins(int playerIndex)
        {
            int maxValueDealer = (from val in this.GetDealerPossibleValues()
                                  where val <= 21
                                  select val).DefaultIfEmpty(0).Max();

            int maxValuePlayer = (from val in this.GetPlayerPossibleValues(playerIndex)
                                  where val <= 21
                                  select val).DefaultIfEmpty(0).Max();

            if (maxValueDealer == maxValuePlayer)
            {
                return 0;
            }
            else
            {
                return (maxValueDealer > maxValuePlayer ? -1 : 1);
            }
        }
    }
}