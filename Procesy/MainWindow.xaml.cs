using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Threading;

namespace Procesy
{
    public partial class MainWindow : Window
    {
        bool canRefresh = true;
        private double refreshRate = 10;
        
        public MainWindow()
        {
            InitializeComponent();
            
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(refreshRate) };
            timer.Tick += ReloadProcesses;
            timer.Start();
            
            var viewModel = (ViewModel)DataContext;
            viewModel.RefreshProcesses();
        }

        private void ReloadProcesses(object? sender, EventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.RefreshProcesses();
        }

        private void TriggerRefresh(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.ClearSelected();
            viewModel.RefreshProcesses();
        }

        private void ToggleBlockRefresh(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.ClearSelected();
            canRefresh = !canRefresh;
            if (!canRefresh)
                (sender as Button).Background = new SolidColorBrush(Colors.DimGray);
            else
                (sender as Button).Background = new SolidColorBrush(Colors.LightGray);
        }

        private void TriggerAutoRefresh(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.ClearSelected();
            
            refreshRate = 10;
            bool parsed = Double.TryParse(RefreshRate.Text, out refreshRate);

            if (!parsed)
            {
                refreshRate = 10;
                RefreshRate.Text = 10.ToString();
            }
        }

        private void SelectProcess(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListBox)sender;
            var viewModel = (ViewModel)DataContext;
            if(list.SelectedItems.Count > 0) 
                viewModel.SelectProcess(((SingleProcess)list.SelectedItems[0]));
        }

        private void FilterProcesses(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.ChangeFilterText(FilterText.Text);
        }

        private void KillProcess(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.KillProcess();
        }

        private void ChangePriority(object sender, RoutedEventArgs e)
        {
            ContextMenu contextMenu = this.FindResource("priorityChange") as ContextMenu;
            contextMenu.PlacementTarget = sender as Button;
            contextMenu.IsOpen = true;
        }

        private void ChangeProcessPriority(object sender, RoutedEventArgs e)
        {
            MenuItem chosen = sender as MenuItem;
            var viewModel = (ViewModel)DataContext;
            viewModel.ChangeProcessPriority(chosen.Header.ToString());
        }

        private void Sort(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = sender as GridViewColumnHeader;
            var viewModel = (ViewModel)DataContext;
            viewModel.Sort(ListBox.Items, column);
        }
    }
}