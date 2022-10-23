using System;
using System.Diagnostics;

namespace Procesy;

public class SingleProcess
{
    private Process process;
    public int id { get; set; }
    public string name { get; set; }
    public ProcessPriorityClass priority { get; set; }
    public ProcessThreadCollection threads { get; set; }
    
    public string mainWindowTitle { get; set; }
    public long memory { get; set; }
    public DateTime startTime { get; set; }
    public int sessionId { get; set; }
    public string? mainModuleName { get; set; }
    public int basePriority { get; set; }
    public bool responding { get; set; }
    
    
    
    
    public SingleProcess(Process process)
    {
        this.process = process;
        id = process.Id;
        name = process.ProcessName;
        threads = process.Threads;
        try
        {

            priority = process.PriorityClass;

            mainWindowTitle = process.MainWindowTitle;
            memory = process.WorkingSet64;
            startTime = process.StartTime;
            sessionId = process.SessionId;
            mainModuleName = process.MainModule?.ModuleName;
            basePriority = process.BasePriority;
            responding = process.Responding;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }
    

    public void KillProcess()
    {
        this.process.Kill();
    }

    public void ChangePriority(ProcessPriorityClass newPriority)
    {
        this.process.PriorityClass = newPriority;
        this.priority = newPriority;
    }

    public void RefreshProcess()
    {
        id = process.Id;
        name = process.ProcessName;
        priority = process.PriorityClass;
        threads = process.Threads;
        
        try
        {

            priority = process.PriorityClass;

            mainWindowTitle = process.MainWindowTitle;
            memory = process.WorkingSet64;
            startTime = process.StartTime;
            sessionId = process.SessionId;
            mainModuleName = process.MainModule?.ModuleName;
            basePriority = process.BasePriority;
            responding = process.Responding;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
}