using System;

namespace ChessAppLibrary.ServerConnection
{
    public class PlayerMovedEvent : EventArgs
    {
        public (int, int) From { get; private set; }
        public (int, int) To { get; private set; }

        public PlayerMovedEvent((int, int) from, (int, int) to)
        {
            From = from;
            To = to;
        }
    }
}