using Microsoft.AspNetCore.SignalR.Client;

namespace ChessAppLibrary
{
    public interface IGameAction
    {
        void SendOn(HubConnection connection);
    }
}