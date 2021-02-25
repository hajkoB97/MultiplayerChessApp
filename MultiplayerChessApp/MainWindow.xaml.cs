using ChessAppLibrary.Chess;
using System;
using System.Windows;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game Game { get; set; }
        Player Creator;
        public MainWindow(Player GameCreator)
        {
            InitializeComponent();
            Creator = GameCreator;
            ChessBoard.Loaded += LoadedGameBoard;
        }

        private void LoadedGameBoard(object sender, RoutedEventArgs e)
        {
            Game = new Game(ChessBoard);

            Game.AddPlayerAndSetupPieces(Creator);
        }

        public MainWindow() : this(new Player("TestName"))
        {
        }
    }
}
