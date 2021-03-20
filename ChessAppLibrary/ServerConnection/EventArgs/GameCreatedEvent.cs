using System;

namespace ChessAppLibrary.ServerConnection
{
    public class GameCreatedEvent : EventArgs
    {
        public string GameId { get; private set; }
        public string JoinToken { get; private set; }

        public GameCreatedEvent(string GameId, string JoinToken) 
        {
            this.GameId = GameId;
            this.JoinToken = JoinToken;
        }

    }
}