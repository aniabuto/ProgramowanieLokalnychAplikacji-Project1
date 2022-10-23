using System;
using System.Diagnostics;

namespace Procesy;

public class ThreadInfo
{
    public int id { get; }
    public DateTime? processorTime { get;}

    public ThreadInfo(ProcessThread thread)
    {
        id = thread.Id;
        try
        {
            processorTime = thread.StartTime;
        }
        catch (Exception e)
        {
            processorTime = null;
            Console.WriteLine(e.Message);
        }

        
    }
}