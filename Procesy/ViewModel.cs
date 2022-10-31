using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;

namespace Procesy;

public class ViewModel
{
    public ObservableCollection<SingleProcess> ProcessesList { get; } = new ObservableCollection<SingleProcess>();
    public ObservableCollection<ThreadInfo> SelectedThreads { get; } = new ObservableCollection<ThreadInfo>();
    public ObservableCollection<ProcessDetail> SelectedDetail { get; } = new ObservableCollection<ProcessDetail>();

    public SingleProcess? SelectedProcess { get; set; }

    public ViewModel()
    {
        SelectedProcess = null;
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
}