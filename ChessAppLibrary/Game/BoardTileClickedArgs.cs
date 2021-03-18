using System;

namespace ChessAppLibrary.Chess
{
    public class BoardTileClickedArgs : EventArgs
    {
        public int ColIndex { get; }
        public int RowIndex { get; }
        public BoardTileClickedArgs(int colIndex, int rowIndex)
        {
            this.ColIndex = colIndex;
            this.RowIndex = rowIndex;
        }
    }
}