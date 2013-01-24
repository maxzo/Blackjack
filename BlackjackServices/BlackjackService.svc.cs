using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using BlackjackContracts;

namespace BlackjackServices
{
    public class BlackjackService : IBlackjackService
    {
        private struct Shuffler
        {
            public int Key;
            public int Value;
        }

        public IEnumerable<Card> GetShuffledDeck()
        {
            const int DECK_SIZE = 52;

            Shuffler[] deck = new Shuffler[DECK_SIZE];

            Random random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < DECK_SIZE; i++)
            {
                deck[i].Key = i;
                deck[i].Value = random.Next();
            }

            var shuffledDeck = from c in deck
                               orderby c.Value
                               select new Card() { Face = (Card.FaceType)(c.Key % 13), Suit = (Card.SuitType)(c.Key / 13) };

            return shuffledDeck;
        }
        
        public IEnumerable<int> GetPossibleValues(IEnumerable<Card> Hand)
        {
            IList<int> values = new List<int>();

            foreach (var card in Hand)
            {
                switch (card.Face)
                {
                    case Card.FaceType.Ace:
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
                    case Card.FaceType.Two:
                    case Card.FaceType.Three:
                    case Card.FaceType.Four:
                    case Card.FaceType.Five:
                    case Card.FaceType.Six:
                    case Card.FaceType.Seven:
                    case Card.FaceType.Eight:
                    case Card.FaceType.Nine:
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
                    case Card.FaceType.Ten:
                    case Card.FaceType.Jack:
                    case Card.FaceType.Queen:
                    case Card.FaceType.King:
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
    }
}