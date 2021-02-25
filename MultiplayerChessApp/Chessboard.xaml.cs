using ChessAppLibrary;
using ChessAppLibrary.Chess;
using ChessAppLibrary.Chess.ChessPieces;
using MultiplayerChessAppUI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultiplayerChessApp
{
    /// <summary>
    /// Interaction logic for ChessBoard.xaml
    /// </summary>
    public partial class ChessBoard : UserControl, IChessBoardUIControl
    {

        public event EventHandler<ChessPieceImageClickedArgs> ChessPieceImageClicked;

        private static int COL_NUM = 8;
        private static int ROW_NUM = 8;
        private int rectSize;
        private List<Shape> indicators;
        private Image[,] pieceImageBoard;

        
        public ChessBoard()
        {
            InitializeComponent();
            pieceImageBoard = new Image[COL_NUM, ROW_NUM];
            indicators = new List<Shape>();

            boardCanvas.MouseMove += Canvas_OnMouseMoved;
            boardCanvas.MouseDown += Canvas_OnMouseClick;
            boardCanvas.MouseLeftButtonUp += Canvas_OnMouseUp;
        }


        private void Canvas_OnMouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        public void Canvas_OnMouseMoved(object sender, System.Windows.Input.MouseEventArgs e)
        {
            
        }

        public void Canvas_OnMouseClick(object sender, System.Windows.Input.MouseEventArgs e)
        {
            var (i, j) = GetIndiciesFromCoords(e.GetPosition(boardCanvas).X, e.GetPosition(boardCanvas).Y);
            if (IsIndexValid(i, j))
            {
                ChessPieceImageClicked?.Invoke(this, new ChessPieceImageClickedArgs(i, j));   
            }


        }

        public void PlaceMoveIndicator(int colIndex, int rowIndex, bool enemy)
        {
            Shape indicator = new Ellipse();

            SolidColorBrush color = new SolidColorBrush();
            color.Color = Color.FromArgb(100, 0, 0, 0);

            if (!enemy)
            {
                indicator.Fill = color;
                indicator.Height = rectSize / 4;
                indicator.Width = rectSize / 4;
            }
            else
            {
                indicator.Stroke = color;
                indicator.StrokeThickness = 8;
                indicator.Height = rectSize;
                indicator.Width = rectSize;
            }

            double centerPointOffset = (rectSize / 2) - (indicator.Width / 2);

            Canvas.SetLeft(indicator, (colIndex * rectSize) + centerPointOffset);
            Canvas.SetTop(indicator, (rowIndex * rectSize) + centerPointOffset);
            boardCanvas.Children.Add(indicator);

            indicators.Add(indicator);
        }

        public void RemoveMoveIndicators()
        {
            foreach(var indicator in indicators)
            {
                boardCanvas.Children.Remove(indicator);
            }
        }
        private (int, int) GetIndiciesFromCoords(double x, double y)
        {
            int i = (int)(Math.Floor((double)(x / rectSize)));
            int j = (int)(Math.Floor((double)(y / rectSize)));

            return (i, j);
        }

        public void PlaceChessPieceImage(IChessPiece piece, int colIndex, int rowIndex)
        {
            if (IsIndexValid(colIndex,rowIndex))
            {
        
                Image pieceImage = new Image();


                var bmi = new BitmapImage();
                bmi.BeginInit();
                    bmi.UriSource = ChessPieceImageFinder.GetImageUriForPiece(piece);
                bmi.EndInit();

                pieceImage.Source = bmi;
               
                pieceImage.Height = rectSize;
                pieceImage.Width = rectSize;
                pieceImage.Stretch = Stretch.Fill;

                double centerPointOffset = (rectSize / 2) - (pieceImage.Width / 2);

                Canvas.SetLeft(pieceImage, (colIndex * rectSize) + centerPointOffset);
                Canvas.SetTop(pieceImage, (rowIndex * rectSize) + centerPointOffset);

                boardCanvas.Children.Add(pieceImage);
                pieceImageBoard[colIndex, rowIndex] = pieceImage;
            }
        }

        public void MovePieceImageToPosition(IChessPiece piece, int colIndex, int rowIndex)
        {
            if(pieceImageBoard[colIndex, rowIndex] != null)
                boardCanvas.Children.Remove(pieceImageBoard[colIndex, rowIndex]);

            double centerPointOffset = (rectSize / 2) - (pieceImageBoard[piece.Coords.Item1, piece.Coords.Item2].Width / 2);

            Canvas.SetLeft(pieceImageBoard[piece.Coords.Item1, piece.Coords.Item2], (colIndex * rectSize) + centerPointOffset);
            Canvas.SetTop(pieceImageBoard[piece.Coords.Item1, piece.Coords.Item2], (rowIndex * rectSize) + centerPointOffset);

            pieceImageBoard[colIndex, rowIndex] = pieceImageBoard[piece.Coords.Item1, piece.Coords.Item2];
            pieceImageBoard[piece.Coords.Item1, piece.Coords.Item2] = null;
        }
        public bool IsIndexValid(int colIndex, int rowIndex)
        {
            if (colIndex < 0 || colIndex > COL_NUM - 1) { return false; }
            if (rowIndex < 0 || rowIndex > ROW_NUM - 1) { return false; }
            return true;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            if (rectSize == 0)
            {
                rectSize = (int)boardCanvas.Height / ROW_NUM;
            }

            bool turn = true;

            Brush fill = Brushes.White;

            for (int i = 0; i < COL_NUM; i++)
            {
                for (int j = 0; j < ROW_NUM; j++)
                {
                    int x = i * rectSize;
                    int y = j * rectSize;
                    Rectangle rect = new Rectangle();
                    fill = turn ? Brushes.White : Brushes.Brown;
                    rect.Width = rectSize;
                    rect.Height = rectSize;
                    rect.Fill = fill;

                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);

                    boardCanvas.Children.Add(rect);

                    turn = !turn;
                }
                turn = !turn;
            }
        }

    }
}
