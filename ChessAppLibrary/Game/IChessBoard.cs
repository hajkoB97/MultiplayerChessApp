using ChessAppLibrary.Chess.ChessPieces;
using MultiplayerChessAppUI;

namespace ChessAppLibrary
{

    public interface IChessBoard
    {
        IChessPiece[,] Board { get; }
        IChessBoardUIControl ChessBoardUI { get; }

        bool IsSquareOccupied(int colIndex, int rowIndex);

        void PlaceChessPiece(IChessPiece piece, int colIndex, int rowIndex);
        void MovePiece((int, int) from, (int, int) to);
        bool IsIndexValid(int colIndex, int rowIndex);

        bool IsMoveValidForPiece(IChessPiece piece, int colIndex, int rowIndex);

    }
}
