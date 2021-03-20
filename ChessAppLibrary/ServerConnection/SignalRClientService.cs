



using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChessAppLibrary.ServerConnection
{
    public class SignalRClientService : IServerConnection
    {
        public HubConnection Connection { get; }
        public GameActionReciver ActionReciever { get; private set; }
        private static readonly object padlock = new object();
        private static SignalRClientService _instance;
        
        public static SignalRClientService Instance
        {
            get
            {
                lock (padlock) ;
                if (_instance == null)
                {
                    _instance = new SignalRClientService();
                }
                return _instance;
            }
        }

        public SignalRClientService()
        {
            Connection = new HubConnectionBuilder().WithUrl("https://localhost:44300/ChessHub").WithAutomaticReconnect().Build();
            Connection.Closed += async (error) =>
            {
                Debug.WriteLine("Disconnected");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await Connection.StartAsync();
            };
            ActionReciever = new GameActionReciver(Connection);
        }

        public async void Connect(Action Connected, Action<string> Failed)
        {
            try
            {
                await Connection.StartAsync();
                Connected();
                Debug.WriteLine("Connected to server");
            }
            catch (HttpRequestException ex)
            {
                Failed(ex.GetBaseException().Message);
                Debug.WriteLine("Could not connect to server");
            }

        }

        public void SendGameAction(IGameAction action)
        {
            action.SendOn(Connection);
        }
    }
}
