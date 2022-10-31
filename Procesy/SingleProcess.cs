using System;
using System.Diagnostics;

namespace Procesy;

public class SingleProcess
{
    private Process process;
    public int id { get; set; }
    public string name { get; set; }
    public string priority { get; set; }
    public ProcessThreadCollection threads { get; set; }
    
    public string mainWindowTitle { get; set; }
    public string memory { get; set; }
    public string startTime { get; set; }
    public string sessionId { get; set; }
    public string? mainModuleName { get; set; }
    public string basePriority { get; set; }
    public string responding { get; set; }
    
    
    
    
    public SingleProcess(Process process)
    {
        this.process = process;
        id = process.Id;
        name = process.ProcessName;
        threads = process.Threads;
        try
        {
            priority = process.PriorityClass.ToString();
        }
        catch (Exception e)
        {
            priority = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            mainWindowTitle = process.MainWindowTitle;
        }
        catch (Exception e)
        {
            mainWindowTitle = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            memory = process.WorkingSet64.ToString();
        }
        catch (Exception e)
        {
            memory = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            startTime = process.StartTime.ToShortDateString();
        }
        catch (Exception e)
        {
            startTime = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            sessionId = process.SessionId.ToString();
        }
        catch (Exception e)
        {
            sessionId = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            mainModuleName = process.MainModule?.ModuleName;
        }
        catch (Exception e)
        {
            mainModuleName = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            basePriority = process.BasePriority.ToString();
        }
        catch (Exception e)
        {
            basePriority = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            responding = process.Responding.ToString();
        }
        catch (Exception e)
        {
            responding = "Access Denied";
            Console.WriteLine(e.Message);
        }

    }
    

    public void KillProcess()
    {
        try
        {
            this.process.Kill();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public void ChangePriority(ProcessPriorityClass newPriority)
    {
        try
        {
            this.process.PriorityClass = newPriority;
            this.priority = process.PriorityClass.ToString();
        }
        catch (Exception e)
        {
            
        }
    }

    public void RefreshProcess()
    {
        id = process.Id;
        name = process.ProcessName;
        threads = process.Threads;
        
        try
        {
            priority = process.PriorityClass.ToString();
        }
        catch (Exception e)
        {
            priority = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            mainWindowTitle = process.MainWindowTitle;
        }
        catch (Exception e)
        {
            mainWindowTitle = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            memory = process.WorkingSet64.ToString();
        }
        catch (Exception e)
        {
            memory = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            startTime = process.StartTime.ToShortDateString();
        }
        catch (Exception e)
        {
            startTime = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            sessionId = process.SessionId.ToString();
        }
        catch (Exception e)
        {
            sessionId = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            mainModuleName = process.MainModule?.ModuleName;
        }
        catch (Exception e)
        {
            mainModuleName = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            basePriority = process.BasePriority.ToString();
        }
        catch (Exception e)
        {
            basePriority = "Access Denied";
            Console.WriteLine(e.Message);
        }
        
        try
        {
            responding = process.Responding.ToString();
        }
        catch (Exception e)
        {
            responding = "Access Denied";
            Console.WriteLine(e.Message);
        }
    }
    
}