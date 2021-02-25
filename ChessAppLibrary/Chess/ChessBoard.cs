using ChessAppLibrary.Chess.ChessPieces;
using MultiplayerChessApp;
using MultiplayerChessAppUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

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

        private void SquareClicked(object sender, ChessPieceImageClickedArgs e)
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

                bool moved = SelectedPiece.AttemptMove(e.ColIndex, e.RowIndex);
                ChessBoardUI.RemoveMoveIndicators();
                SelectedPiece = null;
                
                //int x = SelectedPiece.Coords.Item1;
                //int y = SelectedPiece.Coords.Item2;
                //if(moved)
                //{
                //    ChessBoardUI.MovePieceImageToPosition(Board[x, 7 - y],e.ColIndex, 7 - e.RowIndex);
                //    Board[e.ColIndex, 7 - e.RowIndex] = Board[x, 7 - y];
                //    Board[x, 7-y] = null;
                //    Board[e.ColIndex, 7 - e.RowIndex].Coords = (e.ColIndex, 7 - e.RowIndex);
                //}
            }
            

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
            ChessBoardUI.PlaceChessPieceImage(piece,colIndex,rowIndex);
        }
    }
}
