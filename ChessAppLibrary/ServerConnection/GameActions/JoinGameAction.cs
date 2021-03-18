using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection
{
    public class JoinGameAction : IGameAction
    {
        private string playerName;
        private string playerId;
        private string methodName;
        private string token;

        public JoinGameAction(string playerName, string playerId, string token)
        {
            this.token = token;
            methodName = "JoinGameWithTOken";
            this.playerName = playerName;
            this.playerId = playerId;
        }

        public void SendOn(HubConnection connection)
        {
            connection.SendAsync(methodName, playerName, playerId, token);
        }
    }
}
