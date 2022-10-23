using System;
using System.Diagnostics;

namespace Procesy;

public class ProcessDetail
{
    
    public string key { get; set; }
    public string value { get; set; }

    public ProcessDetail(string key, string value)
    {
        this.key = key;
        this.value = value;
    }
}