
using ChessAppLibrary.Chess;
using ChessAppLibrary.Chess.ChessPieces;
using System;

namespace MultiplayerChessAppUI
{
    public interface IChessBoardUIControl
    {
        event EventHandler<ChessPieceImageClickedArgs> ChessPieceImageClicked; 
        void MovePieceImageToPosition(IChessPiece piece, int colIndex, int rowIndex);

        void PlaceMoveIndicator(int colIndex, int rowIndex, bool enemy);
        void RemoveMoveIndicators();

        void PlaceChessPieceImage(IChessPiece piece, int colIndex, int rowIndex);
    }
}