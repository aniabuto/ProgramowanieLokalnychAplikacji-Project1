using System;
using System.Diagnostics;

namespace Procesy;

public class ThreadInfo
{
    public int id { get; }
    public ThreadState? processorTime { get;}

    public ThreadInfo(ProcessThread thread)
    {
        id = thread.Id;
        try
        {
            //processorTime = thread.StartTime;
            processorTime = thread.ThreadState;
        }
        catch (Exception e)
        {
            processorTime = null;
            Console.WriteLine(e.Message);
        }

        
    }
}