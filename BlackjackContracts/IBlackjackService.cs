using Blackjack.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Blackjack.Contracts
{
    [ServiceContract]
    public interface IBlackjackService
    {
        [OperationContract]
        StartingHands StartGame(int players);

        [OperationContract]
        Card GiveCardToPlayer(int playerIndex);

        [OperationContract]
        Card GiveCardToDealer();

        [OperationContract]
        IEnumerable<int> GetDealerPossibleValues();

        [OperationContract]
        IEnumerable<int> GetPlayerPossibleValues(int playerIndex);

        [OperationContract]
        bool PlayerHasBlackjack(int playerIndex);

        [OperationContract]
        bool PlayerLost(int playerIndex);

        [OperationContract]
        bool DealerStand();

        [OperationContract]
        bool DealerHasBlackjack();

        /// <summary>
        ///  Return 1 if the player wins and -1 if the dealer wins.
        ///  Otherwise returns 0 if there is a draw.
        /// </summary>
        [OperationContract]
        int PlayerWins(int playerIndex);
    }
}
