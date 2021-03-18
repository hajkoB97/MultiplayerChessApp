using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary.ServerConnection.GameActions
{
    public class PlayerMovementAction : IGameAction
    {
        string methodName;
        private string gameId;
        private string id;
        private (int, int) fromCoords;
        private (int, int) toCoords;




        public PlayerMovementAction(string gameId, string id, (int, int) fromCoords, (int, int) toCoords)
        {
            methodName = "NotifyPlayerMovement";
            this.gameId = gameId;
            this.id = id;
            this.fromCoords = fromCoords;
            this.toCoords = toCoords;
        }

        public void SendOn(HubConnection connection)
        {
            connection.SendAsync(methodName, gameId, id, fromCoords.Item1, fromCoords.Item2, toCoords.Item1, toCoords.Item2);

        }
    }
}
