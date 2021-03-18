using MultiplayerChessApp.Models;
using System.Collections.ObjectModel;

namespace MultiplayerChessApp.ViewModels
{
    class ChatViewModel
    {
        private ObservableCollection<ChatMessage> _messages;

        public ObservableCollection<ChatMessage> Messages
        {
            get { return _messages; }
            set
            {
                _messages = value;
            }
        }

    }
}
