using ChessAppLibrary;
using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        IServerConnectionService service = new SignalRClientService();

        bool connectedToGame = false;
        public Window1()
        {
            InitializeComponent();
            service.Connect();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (NameTextBox.Text != "")
            {
                Player player = new Player(NameTextBox.Text);
            }

 
        }


    }
}
