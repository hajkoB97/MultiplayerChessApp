using ChessAppLibrary.Chess.ChessPieces;
using MultiplayerChessAppUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ChessAppLibrary.Chess
{
    public class Game
    {
        public Guid GameId { get; private set; }
        private static Game Instance;
        private Player PlayerOne;
        private Player PlayerTwo;

        public Player GameInstancePlayer { get; private set; }
        private IChessBoard ChessBoard {get; set;}
        private bool GameRunning { get; set; }

        public Game(IChessBoardUIControl chessBoardUI)
        {
            GameId = Guid.NewGuid();
            GameRunning = false;
            this.ChessBoard = new ChessBoard(chessBoardUI,this);
        }

        public Game(IChessBoardUIControl chessBoardUI, Guid id, Player GameCreator)
        {
            this.GameId = id;
            GameRunning = false;
            this.ChessBoard = new ChessBoard(chessBoardUI, this);
            PlayerOne = GameCreator;
        }

        public void AddPlayerAndSetupPieces(Player player)
        {
            bool addedPlayer = AddPlayer(player);
            if (!addedPlayer) return;
            InitPiecesOnBoard(player);
        }
        private bool AddPlayer(Player player)
        {
            if(PlayerOne == null)
            {
                PlayerOne = player;
                PlayerOne.SideColor = ChessPieceColor.WHITE;
                GameInstancePlayer = PlayerOne;
                return true;
            } else if (PlayerTwo == null)
            {
                PlayerTwo = player;
                PlayerTwo.SideColor = ChessPieceColor.BLACK;
                GameInstancePlayer = PlayerTwo;
                return true;
            }
            return false;
        }

        public void PlayerJoinedGame()
        {

        }
        private void InitPiecesOnBoard(Player player)
        {
            string[,] chessPiecePositions = { { "pawn", "pawn", "pawn", "pawn", "pawn", "pawn", "pawn", "pawn" }, { "rook", "knight", "bishop", "queen", "king", "bishop", "knight", "rook" } };
            ChessPieceColor color = player.SideColor;

            for (int i = 6; i <= 7; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    IChessPieceFactory factory = new ChessPieceFactory(ChessBoard);
                    ChessPieceType type;
                    Enum.TryParse<ChessPieceType>(chessPiecePositions[i==6?0:1, j].ToUpper(), out type);

                    IChessPiece piece = factory.GetChessPiece(type, color);
                   
                    GameInstancePlayer.ChessPieces.Add(piece);

                    ChessBoard.PlaceChessPiece(piece,j,i);
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
                    ChessBoard.PlaceChessPiece(factory.GetChessPiece(type, color ), j, i);
                }
            }
        }
    }
}
