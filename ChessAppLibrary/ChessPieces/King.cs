using ChessAppLibrary.ChessPieces;
using System.Collections.Generic;


namespace ChessAppLibrary.Chess.ChessPieces
{
    class King : BasePiece
    {

        public King(IChessBoard board, ChessPieceColor color)
        {
            this.chessBoard = board;
            this.Color = color;
            this.Type = ChessPieceType.KING;
        }

        public King(IChessBoard board, ChessPieceColor color, (int, int) coords) : this(board, color)
        {
            this.Coords = coords;
        }

        public override List<(int, int)> GetValidMoveIndicies()
        {
            int currentI = Coords.Item1;
            int currentJ = Coords.Item2;

            List<(int, int)> validMoves = new List<(int, int)>();

            List<(int, int)> moveOffsets = new List<(int, int)> { (0, -1), (1, -1), (1, 0), (1, 1), (0, 1), (-1, -1), (-1, 0), (-1, 1) };

            foreach (var os in moveOffsets)
            {
                if (chessBoard.IsIndexValid(currentI + os.Item1, currentJ + os.Item2))
                {
                    if (!chessBoard.IsSquareOccupied(currentI + os.Item1, currentJ + os.Item2) || chessBoard.Board[currentI + os.Item1, currentJ + os.Item2].Color != Color)
                        validMoves.Add((currentI + os.Item1, currentJ + os.Item2));
                }

            }
            return validMoves;
        }

    }
}
