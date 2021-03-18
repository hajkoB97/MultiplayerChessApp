
using ChessAppLibrary.ServerConnection;
using System;

namespace ChessAppLibrary
{
    public interface IServerConnection
    {
        PlayerActionReciver ActionReciever { get; }
        void Connect(Action Connected, Action<string> Failed);
        void SendGameAction(IGameAction action);
    }
}