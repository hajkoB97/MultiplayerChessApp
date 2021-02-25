using ChessAppLibrary.Chess.ChessPieces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAppLibrary.ChessPieces
{
    abstract class BasePiece : IChessPiece
    {
        protected IChessBoard chessBoard;

        public ChessPieceType Type { get; protected set; } 
        public ChessPieceColor Color { get; protected set; }

        public (int, int) Coords
        {
            get;
            set;
        }
        
        public abstract List<(int, int)> GetValidMoveIndicies();
        
        ///TODO Checking if King is in Check ot would be after move, 
        public virtual bool AttemptMove(int colIndex, int rowIndex)
        {
            if (GetValidMoveIndicies().Contains((colIndex, rowIndex)))
            {
                chessBoard.ChessBoardUI.MovePieceImageToPosition(this,colIndex, rowIndex);
                chessBoard.Board[Coords.Item1, Coords.Item2] = null;
                chessBoard.Board[colIndex, rowIndex] = this;
                Coords = (colIndex,rowIndex);
                return true;
            }

            return false;
            

        }

        public void MarkValidMovesWithIndicator()
        {
            List<(int, int)> validMoves = GetValidMoveIndicies();
            bool enemy;
            int colIndex;
            int rowIndex;

            foreach( var element in validMoves)
            {
                colIndex = element.Item1;
                rowIndex = element.Item2;

                IChessPiece testedPiece = chessBoard.Board[colIndex, rowIndex];

                if (testedPiece != null && testedPiece.Color != Color)
                {
                    enemy = true;
                } else
                {
                    enemy = false;
                }

                chessBoard.ChessBoardUI.PlaceMoveIndicator(colIndex, rowIndex, enemy);
            }
        }
    }
}
