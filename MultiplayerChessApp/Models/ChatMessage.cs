using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiplayerChessApp.Models
{
    class ChatMessage
    {
        public string PlayerName { get; set; }
        public string Message { get; set; }
        public string TimeString { get; set; }

    }
}
