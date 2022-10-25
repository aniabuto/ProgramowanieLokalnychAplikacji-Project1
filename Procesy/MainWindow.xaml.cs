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
using System.Windows.Threading;

namespace Procesy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            
            // creating filter
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox.ItemsSource);
            collectionView.Filter = MyFilter;
        }

        private bool MyFilter(object obj)
        {
            if (String.IsNullOrEmpty(FilterText.Text))
                return true;
            else
            {
                return ((obj as SingleProcess).name.IndexOf(FilterText.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void ReloadProcesses(object? sender, EventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.RefreshProcesses();
        }

        private void TriggerRefresh(object sender, RoutedEventArgs e)
        {
            if(!canRefresh)
                return;
            var viewModel = (ViewModel)DataContext;
            viewModel.RefreshProcesses();
        }

        private void ToggleBlockRefresh(object sender, RoutedEventArgs e)
        {
            canRefresh = false;
        }

        private void TrggerAutoRefresh(object sender, RoutedEventArgs e)
        {
            canRefresh = true;
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
            CollectionViewSource.GetDefaultView(ListBox.ItemsSource).Refresh();
        }

        private void KillProcess(object sender, RoutedEventArgs e)
        {
            var viewModel = (ViewModel)DataContext;
            viewModel.KillProcess();
        }
    }
}