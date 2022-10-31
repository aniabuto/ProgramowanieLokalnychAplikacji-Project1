﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace Procesy;

public class ViewModel
{
    public ObservableCollection<SingleProcess> ProcessesList { get; } = new ObservableCollection<SingleProcess>();
    public ObservableCollection<ThreadInfo> SelectedThreads { get; } = new ObservableCollection<ThreadInfo>();
    public ObservableCollection<ProcessDetail> SelectedDetail { get; } = new ObservableCollection<ProcessDetail>();

    public SingleProcess? SelectedProcess { get; set; }

    private string filterText = "";
    
    private GridViewColumnHeader sortedColumn = null;
    private SortAdorner sortAdorner = null;

    public ViewModel()
    {
        SelectedProcess = null;
        
        CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ProcessesList);
        collectionView.Filter = MyFilter;
    }

    public void ChangeFilterText(string newText)
    {
        ClearSelected();
        filterText = newText;
        CollectionViewSource.GetDefaultView(ProcessesList).Refresh();
    }

    private bool MyFilter(object obj)
    {
        if (String.IsNullOrEmpty(filterText))
            return true;
        else
        {
            return ((obj as SingleProcess).name.IndexOf(filterText, StringComparison.OrdinalIgnoreCase) >= 0);
        }
    }

    public void RefreshProcesses()
    {
        ProcessesList.Clear();
        foreach (var process in Process.GetProcesses())
        {
            ProcessesList.Add(new SingleProcess(process));
        }

        if(SelectedProcess != null)
            SelectProcess(SelectedProcess);
        else
        {
            SelectedThreads.Clear();
            SelectedDetail.Clear();
        }
    }

    public void ClearSelected()
    {
        SelectedProcess = null;
        SelectedThreads.Clear();
        SelectedDetail.Clear();
    }

    public void SelectProcess(SingleProcess? singleProcess)
    {
        SelectedProcess = singleProcess;
        SelectedThreads.Clear();
        SelectedDetail.Clear();

        foreach (ProcessThread thread in singleProcess.threads)
        {
            var threadInfo = new ThreadInfo(thread);
            SelectedThreads.Add(threadInfo);
        }
        
        SelectedDetail.Add(new ProcessDetail("Process Name", SelectedProcess.name));
        SelectedDetail.Add(new ProcessDetail("Memory usage", SelectedProcess.memory.ToString()));
        SelectedDetail.Add(new ProcessDetail("Main Window Title", SelectedProcess.mainWindowTitle));
        SelectedDetail.Add(new ProcessDetail("Start Time", SelectedProcess.startTime));
        SelectedDetail.Add(new ProcessDetail("Session ID", SelectedProcess.sessionId.ToString()));
        SelectedDetail.Add(new ProcessDetail("Main Module Name", SelectedProcess.mainModuleName ?? ""));
        SelectedDetail.Add(new ProcessDetail("Base Priority", SelectedProcess.basePriority.ToString()));
        SelectedDetail.Add(new ProcessDetail("Responding", SelectedProcess.responding.ToString()));
        SelectedDetail.Add(new ProcessDetail("Number of Threads", SelectedProcess.threads.Count.ToString()));
        
    }

    public void KillProcess()
    {
        if(SelectedProcess != null)
            SelectedProcess.KillProcess();
    }

    public void ChangeProcessPriority(string priority)
    {
        ProcessPriorityClass priorityClass;
        switch (priority)
        {
            case "Idle":
                priorityClass = ProcessPriorityClass.Idle;
                break;
            case "BelowNormal":
                priorityClass = ProcessPriorityClass.BelowNormal;
                break;
            case "Normal":
                priorityClass = ProcessPriorityClass.Normal;
                break;
            case "AboveNormal":
                priorityClass = ProcessPriorityClass.AboveNormal;
                break;
            case "High":
                priorityClass = ProcessPriorityClass.High;
                break;
            case "RealTime":
                priorityClass = ProcessPriorityClass.RealTime;
                break;
            default:
                return;
        }
        if(SelectedProcess != null)
            SelectedProcess.ChangePriority(priorityClass);
    }


    public void Sort(ItemCollection items, GridViewColumnHeader column)
    {
        if (sortedColumn != null)
        {
            AdornerLayer.GetAdornerLayer(sortedColumn).Remove(sortAdorner);
            items.SortDescriptions.Clear();
        }

        ListSortDirection newDirection = ListSortDirection.Ascending;
        if (sortedColumn == column && sortAdorner.Direction == newDirection)
            newDirection = ListSortDirection.Descending;

        sortedColumn = column;
        sortAdorner = new SortAdorner(sortedColumn, newDirection);
        AdornerLayer.GetAdornerLayer(sortedColumn).Add(sortAdorner);
        items.SortDescriptions.Add(new SortDescription(column.Tag.ToString(), newDirection));
            
        CollectionView collectionView = (CollectionView)CollectionViewSource.GetDefaultView(ProcessesList);
        collectionView.SortDescriptions.Add(new SortDescription("name", ListSortDirection.Ascending));
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