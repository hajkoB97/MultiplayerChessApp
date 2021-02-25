using System;

namespace ChessAppLibrary.Chess
{
    public class ChessPieceImageClickedArgs : EventArgs
    {
        public int ColIndex { get; }
        public int RowIndex { get; }
        public ChessPieceImageClickedArgs(int colIndex, int rowIndex )
        {
            this.ColIndex = colIndex;
            this.RowIndex = rowIndex;
        }
    }
}