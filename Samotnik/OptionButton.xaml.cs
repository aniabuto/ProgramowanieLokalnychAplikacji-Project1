using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Project1
{
    public partial class OptionButton : UserControl
    {
        private BoardType chosenBoard = BoardType.NONE;
        public OptionButton()
        {
            InitializeComponent();
        }

        public event EventHandler BoardChosen;
        
        protected virtual void OnButtonClicked(EventArgs e)
        {
            var handler = BoardChosen;
            if (handler != null)
                handler(this, e);
        }

        private void ChooseEuropean(object sender, RoutedEventArgs e)
        {
            chosenBoard = BoardType.EUROPEAN;
            OnButtonClicked(EventArgs.Empty);
        }

        private void ChooseClassic(object sender, RoutedEventArgs e)
        {
            chosenBoard = BoardType.CLASSIC;
            OnButtonClicked(EventArgs.Empty);
        }

        public BoardType GetChosenBoard()
        {
            return chosenBoard;
        }

        private void ClassicBoardButtonClicked(object sender, ExecutedRoutedEventArgs e)
        {
            chosenBoard = BoardType.CLASSIC;
            OnButtonClicked(EventArgs.Empty);
        }

        private void EuropeanBoardButtonClicked(object sender, ExecutedRoutedEventArgs e)
        {
            chosenBoard = BoardType.EUROPEAN;
            OnButtonClicked(EventArgs.Empty);
        }

        private void ButtonCanBeClicked(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}

