using System;
using System.Collections.Generic;
using System.Text;

namespace ChessAppLibrary
{
    public enum GameAction
    {
        CHESSPIECE_MOVED, PLAYER_WON, PLAYER_LOST, PLAYER_JOIN
    }

    public enum ChessPieceType
    {
        PAWN, ROOK, BISHOP, KING, QUEEN, KNIGHT
    }

    public enum ChessPieceColor
    {
        BLACK, WHITE
    }
}
