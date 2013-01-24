using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace BlackjackContracts
{
    [ServiceContract]
    public interface IBlackjackService
    {
        [OperationContract]
        IEnumerable<Card> GetShuffledDeck();

        [OperationContract]
        IEnumerable<int> GetPossibleValues(IEnumerable<Card> Hand);
    }

    [DataContract]
    public class Card
    {
        public enum FaceType { Ace, Two, Three, Four, Five, Six, Seven, Eight, Nine, Ten, Jack, Queen, King };
        public enum SuitType { Club, Diamond, Heart, Spade };

        [DataMember]
        public FaceType Face { get; set; }

        [DataMember]
        public SuitType Suit { get; set; }

        [DataMember]
        public string FaceJSString
        {
            get
            {
                switch (Face)
                {
                    case BlackjackContracts.Card.FaceType.Ace:
                        return "A";
                    case BlackjackContracts.Card.FaceType.Two:
                        return "2";
                    case BlackjackContracts.Card.FaceType.Three:
                        return "3";
                    case BlackjackContracts.Card.FaceType.Four:
                        return "4";
                    case BlackjackContracts.Card.FaceType.Five:
                        return "5";
                    case BlackjackContracts.Card.FaceType.Six:
                        return "6";
                    case BlackjackContracts.Card.FaceType.Seven:
                        return "7";
                    case BlackjackContracts.Card.FaceType.Eight:
                        return "8";
                    case BlackjackContracts.Card.FaceType.Nine:
                        return "9";
                    case BlackjackContracts.Card.FaceType.Ten:
                        return "10";
                    case BlackjackContracts.Card.FaceType.Jack:
                        return "J";
                    case BlackjackContracts.Card.FaceType.Queen:
                        return "Q";
                    case BlackjackContracts.Card.FaceType.King:
                        return "K";
                    default:
                        return "?";
                }
            }

            set { }
        }

        [DataMember]
        public string SuitJSString
        {
            get
            {
                switch (Suit)
                {
                    case BlackjackContracts.Card.SuitType.Club:
                        return "clubs";
                    case BlackjackContracts.Card.SuitType.Diamond:
                        return "diams";
                    case BlackjackContracts.Card.SuitType.Heart:
                        return "hearts";
                    case BlackjackContracts.Card.SuitType.Spade:
                        return "spades";
                    default:
                        return "?";
                }
            }

            set { }
        }
    }
}
