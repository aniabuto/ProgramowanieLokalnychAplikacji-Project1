using System;
using System.Windows;

namespace Project1;

public partial class BoardChooser : Window
{
    private MainWindow mainWindow;
    public BoardChooser(MainWindow mainWindow)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
    }

    public event EventHandler BoardChosen;
        
    protected virtual void OnButtonClicked(EventArgs e)
    {
        var handler = BoardChosen;
        if (handler != null)
            handler(this, e);
    }

    private void BoardChoose_OnBoardChosen(object? sender, EventArgs e)
    {
        mainWindow.chosenBoard = BoardChoose.GetChosenBoard();
        this.Close();
        mainWindow.Show();
        
        OnButtonClicked(EventArgs.Empty);
    }

}