using ChessAppLibrary.Chess.ChessPieces;
using System.Collections.Generic;

namespace ChessAppLibrary.Chess
{
    public interface IPlayer
    {
        Game CurrentGame { get; set; }
        bool IsInGame { get; }
        string PlayerName { get; }
        ChessPieceColor SideColor { get; set; }
    }
}