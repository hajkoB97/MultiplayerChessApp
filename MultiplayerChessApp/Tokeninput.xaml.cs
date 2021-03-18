using System.Windows;

namespace MultiplayerChessAppUI
{
    /// <summary>
    /// Interaction logic for Tokeninput.xaml
    /// </summary>
    public partial class Tokeninput : Window
    {
        public Tokeninput()
        {
            InitializeComponent();
        }

        internal void ShowError()
        {
            TokenError.Visibility = Visibility.Visible;
        }
    }
}
