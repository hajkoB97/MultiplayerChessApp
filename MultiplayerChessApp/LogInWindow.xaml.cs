using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection;
using ChessAppLibrary.ServerConnection.EventAggregator;
using ChessAppLibrary.ServerConnection.GameActions;
using MultiplayerChessApp;
using System;

using System.ComponentModel;

using System.Windows;
using System.Windows.Controls;


namespace MultiplayerChessAppUI
{
    /// <summary>
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window, IHandle<SuccessfulLoginEvent>
    {
        SignalRClientService service;
        Player player;
        public LogInWindow()
        {
            InitializeComponent();
            service = SignalRClientService.Instance;
            service.Connect(
                () => { },
                (error) => { MessageBox.Show(error); }
            );

            //service.ActionReciever.LoginSuccess += OnLoginSuccess;
            EventAggregator ea = EventAggregator.Get();
            ea.Subscribe(this);
        }

        private void OnLoginSuccess(object sender, EventArgs e)
        {
            StartUpWindow window = new StartUpWindow(service, player);
            window.Show();
            this.Close();
        }

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {
            player = new Player(NameTextBox.Text);
            service.SendGameAction(new PlayerLoginAction(player.Name, player.Id));
        }

        private void NickNameTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!LoginBtn.IsEnabled)
            {
                if (NameTextBox.Text.Length > 4)
                {
                    LoginBtn.IsEnabled = true;
                }
                else { LoginBtn.IsEnabled = false; }
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            service.ActionReciever.LoginSuccess -= OnLoginSuccess;
            base.OnClosing(e);
        }

        public void Handle(SuccessfulLoginEvent message)
        {
            StartUpWindow window = new StartUpWindow(service, player);
            window.Show();
            this.Close();
        }
    }
}
