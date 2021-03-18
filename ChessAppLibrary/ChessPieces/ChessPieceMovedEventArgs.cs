using ChessAppLibrary.Chess.ChessPieces;
using System;

namespace MultiplayerChessApp
{
    internal class ChessPieceMovedEventArgs : EventArgs
    {
        public IChessPiece Piece { get; private set; }

        public (int, int) Indecies { get; private set; }

        public ChessPieceMovedEventArgs(IChessPiece piece, (int, int) indecies)
        {
            this.Piece = piece;
            this.Indecies = indecies;
        }

    }
}