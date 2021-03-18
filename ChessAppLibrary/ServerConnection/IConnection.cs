using System;
using System.Threading.Tasks;

namespace ChessAppLibrary.ServerConnection
{
    public interface IConnection
    {
        event Func<Exception, Task> Closed;

        Task SendAsync(string methodName, params object[] args);
        Task StartAsync();

        bool IsConnected();

    }
}
