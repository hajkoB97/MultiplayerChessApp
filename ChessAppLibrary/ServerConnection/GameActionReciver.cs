using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection.EventAggregator;
using Microsoft.AspNetCore.SignalR.Client;
using System;

namespace ChessAppLibrary.ServerConnection
{
    public class GameActionReciver
    {
        private HubConnection connection;
        private IEventAggregator ea = EventAggregator.EventAggregator.Get();
        public event EventHandler<GameFoundEvent> GameFound;
        public event EventHandler<GameFoundEvent> GameCreated;
        public event EventHandler<PlayerMovedEvent> PlayerMoved;
        public event EventHandler<PlayerJoinedEvent> PlayerJoined;
        public event EventHandler GameNotFound;
        public event EventHandler LoginSuccess;
        public GameActionReciver(HubConnection connection)
        {
            this.connection = connection;

            connection.On<string, string, string>("GameFound",
            (string gameId, string creatorId, string creatorName) =>
                ea.Publish(new GameFoundEvent(new Player(creatorName,creatorId) ,gameId)));

            connection.On<string, string>("GameCreated", (string id, string token) => ea.Publish(new GameCreatedEvent(id,token)));

            connection.On("GameNotFound", () => ea.Publish(new GameNotFoundEvent()));

            connection.On("LoginSuccess", () => ea.Publish(new SuccessfulLoginEvent()));

            connection.On<int, int, int, int>("PlayerMoved", (int fromX, int fromY, int toX, int toY) =>
            ea.Publish(new PlayerMovedEvent((fromX, fromY), (toX, toY))));

            connection.On<string, string>("PlayerJoined", (string id, string name) => ea.Publish(new PlayerJoinedEvent(name, id)));
        }
    }
}