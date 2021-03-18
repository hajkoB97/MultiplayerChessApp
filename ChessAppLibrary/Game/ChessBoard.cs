using ChessAppLibrary.Chess.ChessPieces;
using MultiplayerChessAppUI;
using System;

namespace ChessAppLibrary.Chess
{
    class ChessBoard : IChessBoard
    {
        public IChessPiece[,] Board { get; private set; }
        public IChessBoardUIControl ChessBoardUI { get; private set; }

        private IChessPiece SelectedPiece;

        private Game game;

        public ChessBoard(IChessBoardUIControl chessBoardUI, Game game)
        {
            this.ChessBoardUI = chessBoardUI;
            Board = new IChessPiece[8, 8];
            this.game = game;
            ChessBoardUI.ChessPieceImageClicked += SquareClicked;

        }

        private void SquareClicked(object sender, BoardTileClickedArgs e)
        {

            IChessPiece piece = Board[e.ColIndex, e.RowIndex];

            if (CanPlayerSelectPiece(piece))
            {
                ChessBoardUI.RemoveMoveIndicators();
                SelectedPiece = piece;
                SelectedPiece.MarkValidMovesWithIndicator();
                return;
            }

            if (SelectedPiece != null)
            {

                (int, int) fromCoords = SelectedPiece.Coords;


                bool moved = SelectedPiece.AttemptMove(e.ColIndex, e.RowIndex);
                ChessBoardUI.RemoveMoveIndicators();
                if (moved)
                {
                    (int, int) toCoords = SelectedPiece.Coords;
                    game.SendMoveAction(fromCoords, toCoords);


                }
                SelectedPiece = null;


            }


        }

        public void MovePiece((int, int) from, (int, int) to)
        {
            int x = from.Item1;
            int y = from.Item2;

            int newX = to.Item1;
            int newY = to.Item2;

            ChessBoardUI.MovePieceImageToPosition(Board[x, 7 - y], newX, 7 - newY);
            Board[newX, 7 - newY] = Board[x, 7 - y];
            Board[x, 7 - y] = null;
            Board[newX, 7 - newY].Coords = (newX, 7 - newY);
        }

        private bool CanPlayerSelectPiece(IChessPiece piece)
        {
            if (piece == null || !game.GameInstancePlayer.ChessPieces.Contains(piece)) return false;
            return true;
        }
        public bool IsMoveValidForPiece(IChessPiece piece, int colIndex, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public bool IsSquareOccupied(int colIndex, int rowIndex)
        {
            return this.Board[colIndex, rowIndex] != null;
        }

        public bool IsIndexValid(int colIndex, int rowIndex)
        {
            if (colIndex < 0 || colIndex > Board.GetLength(0) - 1) { return false; }
            if (rowIndex < 0 || rowIndex > Board.GetLength(1) - 1) { return false; }
            return true;
        }

        public void PlaceChessPiece(IChessPiece piece, int colIndex, int rowIndex)
        {
            this.Board[colIndex, rowIndex] = piece;
            piece.Coords = (colIndex, rowIndex);
            ChessBoardUI.PlaceChessPieceImage(piece, colIndex, rowIndex);
        }
    }
}
