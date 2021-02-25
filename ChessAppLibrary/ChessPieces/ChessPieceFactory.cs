using ChessAppLibrary.Chess.ChessPieces;

using System;


namespace ChessAppLibrary.Chess
{
    class ChessPieceFactory : IChessPieceFactory
    {
        private IChessBoard board;
        public ChessPieceFactory(IChessBoard board)
        {
            this.board = board;
        }
        public IChessPiece GetChessPiece(ChessPieceType type, ChessPieceColor color)
        {
            IChessPiece piece;

            switch (type)
            {
                case ChessPieceType.PAWN:
                    piece = new Pawn(board, color);
                    break;
                case ChessPieceType.ROOK:
                    piece = new Rook(board, color);
                    break;
                case ChessPieceType.BISHOP:
                    piece = new Bishop(board, color);
                    break;
                case ChessPieceType.KING:
                    piece = new King(board, color);
                    break;
                case ChessPieceType.QUEEN:
                    piece = new Queen(board, color);
                    break;
                case ChessPieceType.KNIGHT:
                    piece = new Knight(board, color);
                    break;
                default:
                    throw new ArgumentException("Called with invalid chess piece type", type.ToString());
            }

            return piece;

        }
    }


}
