using ChessAppLibrary.ChessPieces;
using System;
using System.Collections.Generic;


namespace ChessAppLibrary.Chess.ChessPieces
{
    class Knight : BasePiece
    {
        public Knight(IChessBoard board, ChessPieceColor color)
        {
            this.chessBoard = board;
            this.Color = color;
            this.Type = ChessPieceType.KNIGHT;
        }

        public Knight(IChessBoard board, ChessPieceColor color, (int,int) coords) : this(board, color)
        {
            this.Coords = coords;
        }

        public override List<(int, int)> GetValidMoveIndicies()
        {
            int currentI = Coords.Item1;
            int currentJ = Coords.Item2;

            List<(int, int)> validMoves = new List<(int, int)>();

            int[] X = { 2, 1, -1, -2, -2, -1, 1, 2 };
            int[] Y = { 1, 2, 2, 1, -1, -2, -2, -1 };

            for (int i = 0; i < 8; i++)
            {
                int x = currentI + X[i];
                int y = currentJ + Y[i];

                if (chessBoard.IsIndexValid(x,y))
                    if(!chessBoard.IsSquareOccupied(x, y) || chessBoard.Board[x,y].Color != Color)
                        validMoves.Add((x,y));
            }

            return validMoves;
        }

    }
}
