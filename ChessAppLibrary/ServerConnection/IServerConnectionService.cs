using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary
{
    public interface IServerConnectionService
    {
        HubConnection Connection { get; }
        void Connect();
        bool IsConnected();
        void SendMessage(string messagetype, object obj);
    }
}