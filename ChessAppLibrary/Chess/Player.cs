using ChessAppLibrary.Chess.ChessPieces;
using System;
using System.Collections.Generic;

namespace ChessAppLibrary.Chess
{
    public class Player : IPlayer
    {
        public bool IsInGame { get; private set; }

        public List<IChessPiece> ChessPieces { get; private set; }

        public Game CurrentGame { get; set; }

        private Guid PlayerId { get; set; }

        public string PlayerName { get; private set; }

        public ChessPieceColor SideColor { get; set; }



        public Player(string name)
        {
            PlayerName = name;
            PlayerId = Guid.NewGuid();
            ChessPieces = new List<IChessPiece>();
        }


    }
}
