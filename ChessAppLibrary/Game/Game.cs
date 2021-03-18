using ChessAppLibrary.Chess.ChessPieces;
using ChessAppLibrary.ServerConnection;
using ChessAppLibrary.ServerConnection.GameActions;
using MultiplayerChessAppUI;
using System;

namespace ChessAppLibrary.Chess
{
    public class Game
    {
        public string GameId { get; private set; }
        public string JoinToken { get; private set; }
        private IPlayer PlayerOne;
        private IPlayer PlayerTwo;
        bool IsPrivate = true;

        private IServerConnection service;

        public IPlayer GameInstancePlayer { get; private set; }
        private IChessBoard ChessBoard { get; set; }
        private bool GameRunning { get; set; } = false;

        public Game(IChessBoardUIControl chessBoardUI, string id, IPlayer GameCreator, IPlayer JoinedPlayer, IServerConnection service)
        {
            this.service = service;
            GameId = id;
            this.ChessBoard = new ChessBoard(chessBoardUI, this);
            AddPlayer(GameCreator);
            AddPlayer(JoinedPlayer);
            GameInstancePlayer = JoinedPlayer;
            Init();
        }

        public Game(IChessBoardUIControl chessBoardUI, string id, IPlayer GameCreator, string token, IServerConnection service)
        {
            this.service = service;
            GameId = id;
            this.ChessBoard = new ChessBoard(chessBoardUI, this);
            AddPlayer(GameCreator);
            JoinToken = token;
            service.ActionReciever.PlayerJoined += PlayerJoined;
            GameInstancePlayer = GameCreator;
            Init();
        }

        private void PlayerJoined(object sender, PlayerArgs e)
        {
            AddPlayer(new Player(e.Name, e.Id));
        }

        private void Init()
        {
            service.ActionReciever.PlayerMoved += PlayerMoved;
        }

        public void SendMoveAction((int, int) c, (int, int) d)
        {
            PlayerMovementAction action = new PlayerMovementAction(GameId, GameInstancePlayer.Id, c, d);
            service.SendGameAction(action);
        }
        private void PlayerMoved(object sender, PlayerMovedArgs e)
        {
            ChessBoard.MovePiece(e.From, e.To);
        }

        private bool AddPlayer(IPlayer player)
        {
            if (PlayerOne == null)
            {
                PlayerOne = player;
                PlayerOne.SideColor = ChessPieceColor.WHITE;
                return true;
            }
            else if (PlayerTwo == null)
            {
                PlayerTwo = player;
                PlayerTwo.SideColor = ChessPieceColor.BLACK;
                return true;
            }
            return false;
        }

        public void Render()
        {
            string[,] chessPiecePositions = { { "pawn", "pawn", "pawn", "pawn", "pawn", "pawn", "pawn", "pawn" }, { "rook", "knight", "bishop", "queen", "king", "bishop", "knight", "rook" } };
            ChessPieceColor color = GameInstancePlayer.SideColor;

            for (int i = 6; i <= 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessPieceFactory factory = new ChessPieceFactory(ChessBoard);
                    ChessPieceType type;
                    Enum.TryParse<ChessPieceType>(chessPiecePositions[i == 6 ? 0 : 1, j].ToUpper(), out type);

                    IChessPiece piece = factory.GetChessPiece(type, color);

                    GameInstancePlayer.ChessPieces.Add(piece);

                    ChessBoard.PlaceChessPiece(piece, j, i);
                }
            }

            color = color == ChessPieceColor.WHITE ? ChessPieceColor.BLACK : ChessPieceColor.WHITE;

            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessPieceFactory factory = new ChessPieceFactory(ChessBoard);
                    ChessPieceType type;
                    Enum.TryParse<ChessPieceType>(chessPiecePositions[i == 0 ? 1 : 0, j].ToUpper(), out type);
                    ChessBoard.PlaceChessPiece(factory.GetChessPiece(type, color), j, i);
                }
            }
        }
    }
}
