using System;
using System.Diagnostics;

namespace Procesy;

public class ProcessDetail
{
    
    public string key { get; }
    public string value { get; }

    public ProcessDetail(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}