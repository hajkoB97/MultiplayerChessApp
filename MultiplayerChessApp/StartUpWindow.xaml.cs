using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection;
using MultiplayerChessAppUI;
using System;
using System.ComponentModel;
using System.Windows;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window
    {
        SignalRClientService service;
        Player player;
        public string NickName { get; private set; }

        public StartUpWindow(SignalRClientService service, Player player)
        {
            InitializeComponent();
            this.service = service;
            this.player = player;
            NickName = player.Name;

            service.ActionReciever.GameCreated += GameCreated;
            service.ActionReciever.GameFound += GameFound;
            service.ActionReciever.GameNotFound += GameNotFound;
        }

        private void GameFound(object sender, GameFoundArgs e)
        {
            StartGameWindow(new ChessGameWindow(player, (Player)e.GameCreatorPlayer, e.GameId, service));
        }

        private void GameCreated(object sender, GameFoundArgs e)
        {
            StartGameWindow(new ChessGameWindow(player, e.GameId, e.JoinToken, service));
        }
        private void StartGameWindow(ChessGameWindow gameWindow)
        {
            gameWindow.Show();
            Hide();

            gameWindow.Closed += delegate
            {
                Show();
            };
        }

        private void GameNotFound(object sender, EventArgs e)
        {
            MessageBox.Show("Game Not Found");
        }


        private void JoinRandomBtn_Click(object sender, RoutedEventArgs e)
        {
            service.SendGameAction(new FindGameAction(player.Id, player.Name));
        }

        private void JoinViaTokenBtn_Click(object sender, RoutedEventArgs e)
        {
            Tokeninput input = new Tokeninput();
            input.ShowDialog();

            input.TokenBtn.Click += delegate
            {
                if (input.TokenInputBox.Text.Length == 10)
                {
                    service.SendGameAction(new JoinGameAction(player.Name, player.Id, input.TokenInputBox.Text));
                    input.Close();
                }
                else
                {
                    input.ShowError();
                }
            };
        }

        private void CreateNewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            service.SendGameAction(new CreatePrivateGameAction(player.Name, player.Id));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            service.ActionReciever.GameCreated -= GameCreated;
            service.ActionReciever.GameFound -= GameFound;
            service.ActionReciever.GameNotFound -= GameNotFound;
            base.OnClosing(e);
        }
    }
}
