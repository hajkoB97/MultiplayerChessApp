using ChessAppLibrary.Chess.ChessPieces;

namespace ChessAppLibrary.Chess
{
    interface IChessPieceFactory
    {
        IChessPiece GetChessPiece(ChessPieceType name, ChessPieceColor color);
    }
}
