using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection;
using ChessAppLibrary.ServerConnection.EventAggregator;
using MultiplayerChessAppUI;
using System;
using System.ComponentModel;
using System.Windows;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for StartUpWindow.xaml
    /// </summary>
    public partial class StartUpWindow : Window , IHandle<GameFoundEvent>, IHandle<GameNotFoundEvent>, IHandle<GameCreatedEvent>
    {
        SignalRClientService service;
        Player player;
        IEventAggregator eventAggregator = EventAggregator.Get();
        public string NickName { get; private set; }

        public StartUpWindow(SignalRClientService service, Player player)
        {
            InitializeComponent();
            this.service = service;
            this.player = player;
            NickName = player.Name;

            eventAggregator.Subscribe(this);
        }

        private void GameFound(object sender, GameFoundEvent e)
        {
            StartGameWindow(new ChessGameWindow(player, (Player)e.GameCreatorPlayer, e.GameId, service));
        }

        private void GameCreated(object sender, GameFoundEvent e)
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

        private void JoinRandomBtn_Click(object sender, RoutedEventArgs e)
        {
            service.SendGameAction(new FindGameAction(player.Id, player.Name));
        }

        private void JoinViaTokenBtn_Click(object sender, RoutedEventArgs e)
        {
            Tokeninput input = new Tokeninput();
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
            input.ShowDialog();

        }

        private void CreateNewGameBtn_Click(object sender, RoutedEventArgs e)
        {
            service.SendGameAction(new CreatePrivateGameAction(player.Name, player.Id));
        }
        public void Handle(GameFoundEvent message)
        {
            StartGameWindow(new ChessGameWindow(player, (Player)message.GameCreatorPlayer, message.GameId, service));
        }

        public void Handle(GameNotFoundEvent message)
        {
            MessageBox.Show("Game Not Found");
        }

        public void Handle(GameCreatedEvent message)
        {
            StartGameWindow(new ChessGameWindow(player, message.GameId, message.JoinToken, service));
        }
    }
}
