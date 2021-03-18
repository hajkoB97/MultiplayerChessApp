using ChessAppLibrary.Chess;
using ChessAppLibrary.ServerConnection;
using System.Windows;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for ChessGameWindow.xaml
    /// </summary>
    public partial class ChessGameWindow : Window
    {
        Game Game { get; set; }
        public ChessGameWindow(Player GameJoiner, Player GameCreator, string gameId, SignalRClientService service)
        {
            InitializeComponent();
            Game = new Game(ChessBoard, gameId, GameCreator, GameJoiner, service);

            ChessBoard.Loaded += LoadedGameBoard;
        }

        public ChessGameWindow(Player GameCreator, string gameId, string gameToken, SignalRClientService service)
        {
            InitializeComponent();

            Game = new Game(ChessBoard, gameId, GameCreator, gameToken, service);
            ChessBoard.Loaded += LoadedGameBoard;
        }

        private void LoadedGameBoard(object sender, RoutedEventArgs e)
        {
            Game.Render();
            TokenText.Text = Game.JoinToken;
            PlayerNameText.Text = Game.GameInstancePlayer.Name;
            PlayerIdText.Text = Game.GameInstancePlayer.Id;
            GameIdText.Text = Game.GameId;
        }
    }
}
