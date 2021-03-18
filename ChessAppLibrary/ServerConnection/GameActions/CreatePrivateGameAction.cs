using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection
{
    public class CreatePrivateGameAction : IGameAction
    {
        private string playerName;
        private string playerId;
        private string methodName;

        public CreatePrivateGameAction(string playerName, string playerId)
        {
            methodName = "CreatePrivateGame";
            this.playerName = playerName;
            this.playerId = playerId;
        }

        public void SendOn(HubConnection connection)
        {
            connection.SendAsync(methodName, playerName, playerId);
        }
    }
}
