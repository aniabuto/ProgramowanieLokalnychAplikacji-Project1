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

namespace Procesy
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool canRefresh = true;
        
        public MainWindow()
        {
            InitializeComponent();
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
        }

        private void SelectProcess(object sender, SelectionChangedEventArgs e)
        {
            var list = (ListBox)sender;
            var viewModel = (ViewModel)DataContext;
            viewModel.SelectProcess(((SingleProcess)list.SelectedItems[0]));
        }
    }
}