using ChessAppLibrary.Chess;
using System;

namespace ChessAppLibrary.ServerConnection
{
    public class GameFoundEvent : EventArgs
    {
        public IPlayer GameCreatorPlayer { get; private set; }
        public string GameId { get; private set; }

        public bool IsPrivate { get; private set; } = false;
        public string JoinToken { get; private set; } = "";
        public GameFoundEvent(IPlayer GameCreatorPlayer, string GameId)
        {
            this.GameCreatorPlayer = GameCreatorPlayer;
            this.GameId = GameId;
        }

        public GameFoundEvent(IPlayer GameCreatorPlayer, string GameId, string JoinToken) : this(GameCreatorPlayer, GameId)
        {
            IsPrivate = true;
            this.JoinToken = JoinToken;
        }

    }
}