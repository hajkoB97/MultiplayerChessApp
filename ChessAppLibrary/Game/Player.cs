using ChessAppLibrary.Chess.ChessPieces;
using System;
using System.Collections.Generic;

namespace ChessAppLibrary.Chess
{
    public class Player : IPlayer
    {
        public List<IChessPiece> ChessPieces { get; private set; }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public ChessPieceColor SideColor { get; set; } = ChessPieceColor.WHITE;

        public Player(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
            ChessPieces = new List<IChessPiece>();
        }
        public Player(string name, string id)
        {
            Name = name;
            Id = id;
            ChessPieces = new List<IChessPiece>();
        }


    }
}
