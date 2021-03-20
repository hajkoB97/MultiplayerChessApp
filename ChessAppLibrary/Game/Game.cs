using ChessAppLibrary.Chess.ChessPieces;
using ChessAppLibrary.ServerConnection;
using ChessAppLibrary.ServerConnection.EventAggregator;
using ChessAppLibrary.ServerConnection.GameActions;
using MultiplayerChessAppUI;
using System;

namespace ChessAppLibrary.Chess
{
    public class Game : IHandle<PlayerJoinedEvent>, IHandle<PlayerMovedEvent>
    {
        public string GameId { get; private set; }
        public string JoinToken { get; private set; }
        private IPlayer PlayerOne;
        private IPlayer PlayerTwo;
        bool IsPrivate = true;

        private IEventAggregator eventAggregator = EventAggregator.Get();

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
            eventAggregator.Subscribe(this);
        }

        public Game(IChessBoardUIControl chessBoardUI, string id, IPlayer GameCreator, string token, IServerConnection service)
        {
            this.service = service;
            GameId = id;
            this.ChessBoard = new ChessBoard(chessBoardUI, this);
            AddPlayer(GameCreator);
            JoinToken = token;
            GameInstancePlayer = GameCreator;
            eventAggregator.Subscribe(this);
        }

        public void Handle(PlayerMovedEvent message)
        {
            ChessBoard.MovePiece(message.From, message.To);
        }

        public void Handle(PlayerJoinedEvent message)
        {
            AddPlayer(new Player(message.Name, message.Id));
        }

        public void SendMoveAction((int, int) c, (int, int) d)
        {
            PlayerMovementAction action = new PlayerMovementAction(GameId, GameInstancePlayer.Id, c, d);
            service.SendGameAction(action);
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
