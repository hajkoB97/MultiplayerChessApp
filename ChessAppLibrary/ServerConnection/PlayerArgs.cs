using System;

namespace ChessAppLibrary.ServerConnection
{
    public class PlayerArgs : EventArgs
    {
        public string Name { get; private set; }
        public string Id { get; private set; }

        public PlayerArgs(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
