using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChessAppLibrary.ServerConnection
{
    class TCPConectionService
    {
        TcpClient client = new TcpClient();
        NetworkStream serverStream = default(NetworkStream);
        private List<String> messages = new List<String>();

        private static TCPConectionService INSTANCE;


        public void Connect()
        {
            try
            {
                client.Connect("127.0.0.1", 2345);
                serverStream = client.GetStream();
                Thread thread = new Thread(WaitForMessage);
                thread.Start();
                return ;
            } catch (SocketException e)
            {
                return ;
            }
        }

        public bool IsConnected()
        {
            if(client.Connected)
            {
                return true;
            }
            return false;
        }


        public void SendMessage(string messagetype, object obj)
        {
            byte[] byteMessage = Encoding.UTF8.GetBytes(messagetype);

            serverStream.Write(byteMessage, 0, byteMessage.Length);
            serverStream.Flush();
        }

        private void WaitForMessage()
        {
            string returnData;

            while (true)
            {
                serverStream = client.GetStream();
                int bufferSize = client.ReceiveBufferSize;

                byte[] buffer = new byte[bufferSize];
                try
                {
                    serverStream.Read(buffer, 0, bufferSize);
                } catch(System.IO.IOException se)
                {
                    Debug.Write(se.Message);
                    break;
                }

                returnData = Encoding.UTF8.GetString(buffer);


                if (returnData != "")
                {
                    GotMessage(returnData);
                }

            }

        }


        private void GotMessage(string message)
        {
            this.messages.Add(message);
        }
    }
}
