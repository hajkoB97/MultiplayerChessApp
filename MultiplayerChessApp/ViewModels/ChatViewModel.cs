using MultiplayerChessApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerChessApp.ViewModels
{
    class ChatViewModel
    {
        private ObservableCollection<ChatMessage> _messages;

        public ObservableCollection<ChatMessage> Messages
        {
            get { return _messages; }
            set { 
                _messages = value;
            }
        }

    }
}
