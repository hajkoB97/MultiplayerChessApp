



using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessAppLibrary.ServerConnection
{
    public class SignalRClientService : IServerConnectionService
    {
        public HubConnection Connection { get; }
        public event EventHandler<GameFoundArgs> GameFound;
        public event EventHandler GameNotFound;


        public SignalRClientService()
        {

            Connection = new HubConnectionBuilder().WithUrl("https://localhost:44300/ChessHub").WithAutomaticReconnect().Build();
            Connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };

            Connection.On<string>("Message", GotMessage);
            
        }

        public void GotMessage(string message)
        {
            Debug.WriteLine("Got message", message);
        }

        public async void Connect()
        {
            await Connection.StartAsync();
        }

        public bool IsConnected()
        {
            return Connection.State == HubConnectionState.Connected;
        }

        public void SendMessage(string messagetype, object obj)
        {
            Connection.SendAsync("Send", obj);
        }
    }
}
