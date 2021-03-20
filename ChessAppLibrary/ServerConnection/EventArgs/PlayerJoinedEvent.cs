using System;

namespace ChessAppLibrary.ServerConnection
{
    public class PlayerJoinedEvent : EventArgs
    {
        public string Name { get; private set; }
        public string Id { get; private set; }

        public PlayerJoinedEvent(string name, string id)
        {
            Name = name;
            Id = id;
        }
    }
}
