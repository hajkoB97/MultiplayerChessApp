using ChessAppLibrary.Utils;
using ChessAppServer.Storage;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ChessAppServer.Hubs
{
    public class ChessGameHub : Hub
    {
        private readonly TestDataStorage dataStorage = TestDataStorage.GetStorage();

        public async void PlayerLogin(string playerName, string playerId)
        {
            dataStorage.Players.Add(playerId, (playerName, Context.ConnectionId, false));
            await Groups.AddToGroupAsync(Context.ConnectionId, playerId);
            ;
            await Clients.Caller.SendAsync("LoginSuccess");
        }

        public void NotifyPlayerMovement(string gameId, string playerId, int a, int b, int c, int d)
        {
            var gameTuple = dataStorage.PrivateGames[gameId];
            string enemyId;


            if (gameTuple.Item2 == playerId)
            {
                enemyId = gameTuple.Item3;
            }
            else
            {
                enemyId = gameTuple.Item2;
            }

            if (enemyId == null) return;

            Clients.Group(enemyId).SendAsync("PlayerMoved", a, b, c, d);
        }

        public void CreatePrivateGame(string playerName, string playerId)
        {
            string gameId = Guid.NewGuid().ToString();
            string gameToken = TokenGenerator.GenerateGameToken();

            dataStorage.PrivateGames.Add(gameId, (gameToken, playerId, null));
            Clients.Caller.SendAsync("GameCreated", gameId, gameToken);
        }


        public void JoinGameWithTOken(string playerName, string playerId, string token)
        {
            (string gameId, string gameCreatorId, string gameCreatorName) = dataStorage.FindGameByToken(token);

            if (gameId == null)
            {
                Clients.Caller.SendAsync("GameNotFound");
            }
            else
            {
                Clients.Caller.SendAsync("GameFound", gameId, gameCreatorId, gameCreatorName);
                Clients.Group(gameCreatorId).SendAsync("PlayerJoined", playerId, playerName);
                dataStorage.PrivateGames[gameId] = (token, playerId, gameCreatorId);
            }

        }

        public override Task OnConnectedAsync()
        {
            Debug.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Debug.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }
    }
}
