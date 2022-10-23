using System.Windows;

namespace Project1;

public partial class EndGameWindow : Window
{

    private MainWindow mainWindow;
    // public EndGameWindow()
    // {
    //     InitializeComponent();
    // }

    public EndGameWindow(MainWindow mainWindow)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
    }
    
    private void NewGameButton_OnClick(object sender, RoutedEventArgs e)
    {
        mainWindow.RestartGame();
        this.Close();
    }

    private void EndGameButton_OnClick(object sender, RoutedEventArgs e)
    {
        mainWindow.Close();
        this.Close();
    }
}