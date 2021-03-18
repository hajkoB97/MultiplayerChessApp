using ChessAppLibrary.Chess.ChessPieces;
using System.Collections.Generic;

namespace ChessAppLibrary.Chess
{
    public interface IPlayer
    {
        List<IChessPiece> ChessPieces { get; }

        string Id { get; }

        string Name { get; }

        ChessPieceColor SideColor { get; set; }
    }
}