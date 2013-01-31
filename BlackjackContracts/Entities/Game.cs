using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Contracts.Entities
{
    [DataContract]
    public class Game
    {
        [DataMember]
        public IEnumerable<Card> Deck { get; set; }

        [DataMember]
        public Player Dealer { get; set; }

        [DataMember]
        public IEnumerable<Player> Players { get; set; }

        public Game(int players)
        {
            this.Deck = GetShuffledDeck();
            this.Dealer = new Player() { Type = PlayerType.Dealer };
            this.Players = new List<Player>();

            for (int i = 0; i < players; i++)
            {
                ((List<Player>)(this.Players)).Add(new Player() { Type = PlayerType.Normal });
            }
        }

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
                               select new Card() { Face = (CardFace)(c.Key % 13), Suit = (CardSuit)(c.Key / 13) };

            return shuffledDeck.ToList();
        }

        public Card GiveCardToDealer()
        {
            var card = this.Deck.First();
            ((List<Card>)(this.Dealer.Hand)).Add(this.Deck.First());
            ((List<Card>)(this.Deck)).RemoveAt(0);
            return card;
        }

        public Card GiveCardToPlayer(int playerIndex)
        {
            var card = this.Deck.First();
            ((List<Card>)(this.Players.ElementAt(playerIndex).Hand)).Add(card);
            ((List<Card>)(this.Deck)).RemoveAt(0);
            return card;
        }
    }
}
