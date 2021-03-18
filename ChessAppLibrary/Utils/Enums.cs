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

    public enum GameEvent
    {
        GameFound, GameNotFound, GameCreated, PlayerMoved, PlayerWon, PlayerMessage
    }
}
