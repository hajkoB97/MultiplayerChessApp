using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection.GameActions
{
    public class PlayerLoginAction : IGameAction
    {
        private string methodName;
        private string playerName;
        private string playerId;

        public PlayerLoginAction(string playerName, string playerId)
        {
            methodName = "PlayerLogin";
            this.playerName = playerName;
            this.playerId = playerId;
        }

        public void SendOn(HubConnection connection)
        {
            connection.SendAsync(methodName, playerName, playerId);
        }


    }
}