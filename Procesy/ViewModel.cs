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

    private void ReloadProcesses(object? sender, EventArgs e)
    {
        RefreshProcesses();
    }


    public void RefreshProcesses()
    {
        var ids = ProcessesList.Select(process => process.id).ToList();
        
        foreach (var process in Process.GetProcesses())
        {
            if (!ids.Remove(process.Id))
            {
                ProcessesList.Add(new SingleProcess(process));
            }
        }

        foreach (var id in ids)
        {
            var leftoverProcess = ProcessesList.First(process => process.id == id);
            ProcessesList.Remove(leftoverProcess);
        }

        if(SelectedProcess != null)
            SelectProcess(SelectedProcess);
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
    
}