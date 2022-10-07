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
        
        // SolidColorBrush brushTaken = new SolidColorBrush();
        // SolidColorBrush brushFree = new SolidColorBrush();
        // SolidColorBrush brushCorrect = new SolidColorBrush();
        
        public MainWindow()
        {
            InitializeComponent();
            // CreateBoard();
            BeginGame();
            
        }

        private void BeginGame()
        {
            size = 7;
            board = new Board(size, BoardType.ENGLISH);
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
                    Grid.SetRow(field, j + 1);
                    Grid.SetColumn(field, i + 1);
                    fieldPlane.Children.Add(field);
                }
            }
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

        // private void CreateBoard()
        // {
        //     
        //     size = 7;
        //     this.Width = (size + 4) * 60;
        //     this.Height = (size + 4) * 60;
        //     // var borderRow = new RowDefinition(){ Height = new GridLength(3, GridUnitType.Star) };
        //     fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) });
        //     fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
        //     for (int r = 0; r < size; r++)
        //     {
        //         fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        //         fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star) });
        //     }
        //     fieldPlane.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(3, GridUnitType.Star) });
        //     fieldPlane.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });
        //
        //     int crossSize = size / 2;
        //     int freeCells = (size - crossSize) / 2;
        //
        //     brushTaken = new SolidColorBrush();
        //     brushFree = new SolidColorBrush();
        //     brushCorrect = new SolidColorBrush();
        //     
        //     brushTaken.Color = Color.FromRgb(250, 50, 50);
        //     brushCorrect.Color = Color.FromRgb(150, 150, 250);
        //     brushFree.Color = Color.FromRgb(150, 150, 150);
        //
        //     
        //     board = new ClassicBoard(size);
        //     board.PopulateBoard();
        //     
        //     for (int i = 0; i < size; i++)
        //     {
        //         for (int j = 0; j < size; j++)
        //         {
        //             var field = board.GetField(i, j);
        //             if (!field.IsField)
        //                 continue;
        //             field.Background = field.IsTaken ? brushTaken : brushFree;
        //             field.Click += ButtonOnClick;
        //             Grid.SetRow(field, j + 1);
        //             Grid.SetColumn(field, i + 1);
        //             fieldPlane.Children.Add(field);
        //         }
        //     }
        // }
        //
        // private void Redraw()
        // {
        //     for (int i = 0; i < size; i++)
        //     {
        //         for (int j = 0; j < size; j++)
        //         {
        //             var field = board.GetField(i, j);
        //             if (!field.IsField)
        //                 continue;
        //             field.Background = field.IsTaken ? brushTaken : brushFree;
        //         }
        //     }
        // }
        //
        // private void ButtonOnClick(object sender, RoutedEventArgs e)
        // {
        //     Field field = (Field) sender;
        //
        //     if (!board.GetCorrect().Contains(field))
        //     {
        //         board.UnmarkCorrect();
        //         Redraw();
        //         board.ShowCorrectMoves(field.I, field.J);
        //         foreach (var correct in board.GetCorrect())
        //         {
        //             correct.Background = brushCorrect;
        //         }
        //
        //         selected = field;
        //     }
        //     else
        //     {
        //         board.UnmarkCorrect();
        //         board.Move(selected, field);
        //         Redraw();
        //         selected = null;
        //     }
        // }

        // private void BeginGame()
        // {
        //     Board board = new Board(7, BoardType.ENGLISH);
        // }
    }
}