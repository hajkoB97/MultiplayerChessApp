using ChessAppLibrary.ChessPieces;
using System.Collections.Generic;


namespace ChessAppLibrary.Chess.ChessPieces
{
    class Queen : BasePiece
    {
        public Queen(IChessBoard board, ChessPieceColor color)
        {
            this.chessBoard = board;
            this.Color = color;
            this.Type = ChessPieceType.QUEEN;
        }

        public Queen(IChessBoard board, ChessPieceColor color, (int, int) coords) : this(board, color)
        {
            this.Coords = coords;
        }

        public override List<(int, int)> GetValidMoveIndicies()
        {
            int currentI;
            int currentJ;

            List<(int, int)> validMoves = new List<(int, int)>();

            (int, int)[] directionOffsets = { (0, -1), (1, 0), (0, 1), (-1, 0), (1, -1), (1, 1), (-1, 1), (-1, -1) };

            foreach (var dir in directionOffsets)
            {
                currentI = Coords.Item1 + dir.Item1;
                currentJ = Coords.Item2 + dir.Item2;
                for (int i = 0; i < 8; i++)
                {
                    if (!chessBoard.IsIndexValid(currentI, currentJ)) break;
                    if (!chessBoard.IsSquareOccupied(currentI, currentJ))
                    {
                        validMoves.Add((currentI, currentJ));
                        currentI += dir.Item1;
                        currentJ += dir.Item2;
                    }
                    else if (chessBoard.Board[currentI, currentJ].Color != Color)
                    {
                        validMoves.Add((currentI, currentJ));
                        break;
                    }

                }
            }

            return validMoves;
        }

    }
}
