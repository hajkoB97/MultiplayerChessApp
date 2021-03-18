using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection.GameActions
{
    public class PlayerMoveAction : IGameAction
    {
        private string methodName;
        private string gameId;
        private string playerId;
        private (int, int) from;
        private (int, int) to;

        public PlayerMoveAction(string gameId, string playerId, (int, int) from, (int, int) to)
        {
            methodName = "PlayerMoved";
            this.gameId = gameId;
            this.playerId = playerId;
            this.from = from;
            this.to = to;
        }

        public void SendOn(HubConnection connection)
        {

            connection.SendAsync(methodName, gameId, playerId, from.Item1, from.Item2, to.Item1, to.Item2);
        }


    }
}