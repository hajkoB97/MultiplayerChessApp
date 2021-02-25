using ChessAppLibrary.Chess;
using System;

namespace ChessAppLibrary.ServerConnection
{
    public class GameFoundArgs : EventArgs
    {
        public IPlayer GameCreatorPlayer { get; private set; }
        public Guid GameId { get; private set; }
        public GameFoundArgs(IPlayer GameCreatorPlayer, Guid GameId)
        {
            this.GameCreatorPlayer = GameCreatorPlayer;
            this.GameId = GameId;
        }
    }
}