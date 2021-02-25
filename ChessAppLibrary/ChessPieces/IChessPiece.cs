using System.Collections.Generic;


namespace ChessAppLibrary.Chess.ChessPieces
{
    public interface IChessPiece
    {
        ChessPieceColor Color { get; }
        ChessPieceType Type { get; }
        (int,int) Coords { get; set; }
        List<(int,int)> GetValidMoveIndicies();
        void MarkValidMovesWithIndicator();
        bool AttemptMove(int colIndex, int rowIndex);
    }
}
