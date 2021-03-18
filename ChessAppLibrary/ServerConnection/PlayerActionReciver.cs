using ChessAppLibrary.Chess;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ChessAppLibrary.ServerConnection
{
    public class PlayerActionReciver
    {
        private HubConnection connection;
        public event EventHandler<GameFoundArgs> GameFound;
        public event EventHandler<GameFoundArgs> GameCreated;
        public event EventHandler<PlayerMovedArgs> PlayerMoved;
        public event EventHandler<PlayerArgs> PlayerJoined;
        public event EventHandler GameNotFound;
        public event EventHandler LoginSuccess;
        public PlayerActionReciver(HubConnection connection)
        {
            this.connection = connection;

            connection.On<string, string, string>("GameFound", OnGameFound);
            connection.On<string, string>("GameCreated", OnGameCreated);
            connection.On("GameNotFound", OnGameNotFound);
            connection.On("LoginSuccess", OnLoginSuccess);
            connection.On<int, int, int, int>("PlayerMoved", OnPlayerMoved);
            connection.On<string, string>("PlayerJoined", OnPlayerJoined);
        }

        private void OnPlayerJoined(string id, string name)
        {
            PlayerJoined?.Invoke(this, new PlayerArgs(name, id));
        }

        private void OnPlayerMoved(int fromX, int fromY, int toX, int toY)
        {
            PlayerMoved?.Invoke(this, new PlayerMovedArgs((fromX, fromY), (toX, toY)));
        }

        private void OnLoginSuccess()
        {
            LoginSuccess?.Invoke(this, EventArgs.Empty);
        }
        private void OnGameNotFound()
        {
            GameNotFound?.Invoke(this, EventArgs.Empty);
        }

        private void OnGameCreated(string gameId, string gameToken)
        {
            GameCreated?.Invoke(this, new GameFoundArgs(null, gameId, gameToken));
        }

        private void OnGameFound(string gameId, string playerId, string playername)
        {
            GameFound?.Invoke(this, new GameFoundArgs(new Player(playername, playerId), gameId));
        }
    }
}