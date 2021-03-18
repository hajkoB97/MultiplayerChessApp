using ChessAppLibrary.ChessPieces;
using System;
using System.Collections.Generic;

namespace ChessAppLibrary.Chess.ChessPieces
{
    class Pawn : BasePiece
    {
        private bool moved;

        public Pawn(IChessBoard board, ChessPieceColor color)
        {
            this.moved = false;
            this.chessBoard = board;
            this.Color = color;
            this.Type = ChessPieceType.PAWN;
        }

        public Pawn(IChessBoard board, ChessPieceColor color, (int, int) coords) : this(board, color)
        {
            this.Coords = coords;
        }

        public override List<(int, int)> GetValidMoveIndicies()
        {
            int currentI = Coords.Item1;
            int currentJ = Coords.Item2;

            List<ValueTuple<int, int>> validMoves = new List<ValueTuple<int, int>>();

            if (chessBoard.IsIndexValid(currentI, currentJ - 1) && !chessBoard.IsSquareOccupied(currentI, currentJ - 1))
            {
                validMoves.Add((currentI, currentJ - 1));

                // if not moved yet can move 2 step forward
                if (chessBoard.IsIndexValid(currentI, currentJ - 2) && !moved && !chessBoard.IsSquareOccupied(currentI, currentJ - 2))
                {
                    validMoves.Add((currentI, currentJ - 2));
                }
            }

            //check En passant rule
            if (chessBoard.IsIndexValid(currentI + 1, currentJ - 1) && chessBoard.IsSquareOccupied(currentI + 1, currentJ - 1))
            {
                if (chessBoard.Board[currentI + 1, currentJ - 1].Color != Color)
                {
                    validMoves.Add((currentI + 1, currentJ - 1));
                }
            }

            if (chessBoard.IsIndexValid(currentI - 1, currentJ - 1) && chessBoard.IsSquareOccupied(currentI - 1, currentJ - 1))
            {
                if (chessBoard.Board[currentI - 1, currentJ - 1].Color != Color)
                {
                    validMoves.Add((currentI - 1, currentJ - 1));
                }
            }

            return validMoves;
        }

        public override bool AttemptMove(int colIndex, int rowIndex)
        {
            if (base.AttemptMove(colIndex, rowIndex))
            {
                if (!moved) moved = true;
                return true;
            }

            return false;
        }
    }
}
