using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection
{
    public class FindGameAction : IGameAction
    {
        private readonly string playerName;
        private readonly string playerId;
        private readonly string methodName;

        public FindGameAction(string playerName, string playerId)
        {
            methodName = "FindGameForPlayer";
            this.playerName = playerName;
            this.playerId = playerId;
        }

        public void SendOn(HubConnection connection)
        {
            connection.SendAsync(methodName, playerName, playerId);
        }
    }
}
