using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    private LoggerModes _loggerMode = LoggerModes.Debug;
    public bool Initialized { get; private set; }
    void Start()
    {
        print("Logger is up and working!");
        Initialized = true;
    }

    public void Trace(object sender, object message) 
    {
        if(_loggerMode == LoggerModes.Trace) PrintOnConsole(sender, message);
    }

    public void Debug(object sender, object message)
    {
        if(_loggerMode <= LoggerModes.Debug) PrintOnConsole(sender, message);
    }

    public void Info(object sender, object message)
    {
        if(_loggerMode <= LoggerModes.Info) PrintOnConsole(sender, message);
    }

    public void Error(object sender, object message)
    {
        if(_loggerMode <= LoggerModes.Error) PrintOnConsole(sender, message);
    }

    private void PrintOnConsole(object sender, object message)
    {
        print($"{sender.GetType().Name} | {message.ToString()}");
    }
}
