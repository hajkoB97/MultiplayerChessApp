using ChessAppLibrary.Chess.ChessPieces;
using System;

namespace ChessAppLibrary.Chess
{
    public class ChessPieceImageFinder
    {

        public static Uri GetImageUriForPiece(IChessPiece piece)
        {
            string colorPrefix;
            if (piece.Color == ChessPieceColor.BLACK)
            {
                colorPrefix = "b_";
            }
            else colorPrefix = "w_";

            return new Uri("pack://application:,,,/MultiplayerChessAppUI;component/Resources/" + colorPrefix + piece.Type.ToString().ToLower() + ".png");
        }

    }
}
