using System;

namespace ChessAppLibrary.ServerConnection
{
    public class PlayerMovedArgs : EventArgs
    {
        public (int, int) From { get; private set; }
        public (int, int) To { get; private set; }

        public PlayerMovedArgs((int, int) from, (int, int) to)
        {
            From = from;
            To = to;
        }
    }
}