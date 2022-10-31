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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool canRefresh = true;
        private double refreshRate = 10;
        private GridViewColumnHeader sortedColumn = null;
        private SortAdorner sortAdorner = null;
        
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

        private void TrggerAutoRefresh(object sender, RoutedEventArgs e)
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
            viewModel.ClearSelected();
            CollectionViewSource.GetDefaultView(ListBox.ItemsSource).Refresh();
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
            if (sortedColumn != null)
            {
                AdornerLayer.GetAdornerLayer(sortedColumn).Remove(sortAdorner);
                ListBox.Items.SortDescriptions.Clear();
            }

            ListSortDirection newDirection = ListSortDirection.Ascending;
            if (sortedColumn == column && sortAdorner.Direction == newDirection)
                newDirection = ListSortDirection.Descending;

            sortedColumn = column;
            sortAdorner = new SortAdorner(sortedColumn, newDirection);
            AdornerLayer.GetAdornerLayer(sortedColumn).Add(sortAdorner);
            ListBox.Items.SortDescriptions.Add(new SortDescription(column.Tag.ToString(), newDirection));
            
            CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ListBox.ItemsSource);
            collectionView.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));
        }
    }
    
    public class SortAdorner : Adorner
    {
        private static Geometry ascGeometry =
            Geometry.Parse("M 0 4 L 3.5 0 L 7 4 Z");

        private static Geometry descGeometry =
            Geometry.Parse("M 0 0 L 3.5 4 L 7 0 Z");

        public ListSortDirection Direction { get; private set; }

        public SortAdorner(UIElement element, ListSortDirection dir)
            : base(element)
        {
            this.Direction = dir;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            if(AdornedElement.RenderSize.Width < 20)
                return;

            TranslateTransform transform = new TranslateTransform
            (
                AdornedElement.RenderSize.Width - 15,
                (AdornedElement.RenderSize.Height - 5) / 2
            );
            drawingContext.PushTransform(transform);

            Geometry geometry = ascGeometry;
            if(this.Direction == ListSortDirection.Descending)
                geometry = descGeometry;
            drawingContext.DrawGeometry(Brushes.Navy, null, geometry);

            drawingContext.Pop();
        }
    }
}