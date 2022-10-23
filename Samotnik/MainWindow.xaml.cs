using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Project1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // private ClassicBoard board;
        private int size;
        private Board board;
        public static EndGameWindow EndGameWindow;
        public BoardType chosenBoard = 0;
        private BoardChooser boardChooser;

        public static short[,] boardtemplate;
        
        // SolidColorBrush brushTaken = new SolidColorBrush();
        // SolidColorBrush brushFree = new SolidColorBrush();
        // SolidColorBrush brushCorrect = new SolidColorBrush();
        
        public MainWindow()
        {
            InitializeComponent();
            
            this.Hide();
            
            boardChooser = new BoardChooser(this);
            boardChooser.BoardChosen += BoardChooserOnBoardChosen;
            boardChooser.Show();

            
            EndGameWindow = new EndGameWindow(this);
            EndGameWindow.Hide();

        }

        private void BoardChooserOnBoardChosen(object? sender, EventArgs e)
        {
            if (chosenBoard == BoardType.CLASSIC)
            {
                boardtemplate = new short[7, 7]
                {
                    {-1, -1, 1, 1, 1, -1, -1 },
                    {-1, -1, 1, 1, 1, -1, -1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 0, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {-1, -1, 1, 1, 1, -1, -1 },
                    {-1, -1, 1, 1, 1, -1, -1 }
                };
            }
            else
            {
                boardtemplate = new short[7, 7]
                {
                    {-1, -1, 1, 1, 1, -1, -1 },
                    {-1, 1, 1, 1, 1, 1, -1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {1, 1, 1, 0, 1, 1, 1 },
                    {1, 1, 1, 1, 1, 1, 1 },
                    {-1, 1, 1, 1, 1, 1, -1 },
                    {-1, -1, 1, 1, 1, -1, -1 }
                };
                
            }
            BeginGame();
        }

        private void BeginGame()
        {
            size = 7;
            board = new Board(size);
            this.Width = (size + 4) * 60;
            this.Height = (size + 4) * 60;
            
            fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) });
            fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            for (int r = 0; r < size; r++)
            {
                fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
                fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
            }
            fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) });
            fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
            
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var field = Board.GetPlace(new Coords(i, j));
                    if (!field.IsAField())
                        continue;
                    field.Click += ButtonOnClick;
                    field.Template = FindResource("ButtonTemplate") as ControlTemplate;
                    Grid.SetRow(field, j + 1);
                    Grid.SetColumn(field, i + 1);
                    fieldPlane.Children.Add(field);
                }
            }
        }

        public void RestartGame()
        {
            this.Hide();
            fieldPlane.Children.Clear();
            fieldPlane.ColumnDefinitions.Clear();
            fieldPlane.RowDefinitions.Clear();
            
            boardChooser = new BoardChooser(this);
            boardChooser.BoardChosen += BoardChooserOnBoardChosen;
            boardChooser.Show();
            
            //BeginGame();
        }

        public void RestartGameNoChoice()
        {
            fieldPlane.Children.Clear();
            fieldPlane.ColumnDefinitions.Clear();
            fieldPlane.RowDefinitions.Clear();
            
            BeginGame();
        }
        
        private void ButtonOnClick(object sender, RoutedEventArgs e)
        {
            Place field = (Place) sender;
            if(Board.SelectedPawn == Board.NullPawn)
            {
                if(Board.GetPawn(field).Equals(Board.NullPawn))
                {
                    Board.ChangeSelected(Board.NullPawn);
                    return;
                }
                Board.GetPawn(field).SelectPawn();
            }
            else
            {
                Board.SelectedPawn.MovePawn(field);
            }
        }

        private void RestartChoosingBoard(object sender, RoutedEventArgs e)
        {
            RestartGame();
        }

        private void RestartNoChoosingBoard(object sender, RoutedEventArgs e)
        {
            RestartGameNoChoice();
        }
    }
}