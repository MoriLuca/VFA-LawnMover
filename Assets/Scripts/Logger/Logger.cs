using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logger : MonoBehaviour
{
    private LoggerModes _loggerMode = LoggerModes.Trace;
    
    void Start()
    {
        print("Logger is up and working!");
    }

    public void Trace(object sender, string message) 
    {
        if(_loggerMode >= LoggerModes.Trace) PrintOnConsole(sender, message);
    }

    public void Debug(object sender, string message)
    {
        if(_loggerMode >= LoggerModes.Debug) PrintOnConsole(sender, message);
    }

    public void Info(object sender, string message)
    {
        if(_loggerMode >= LoggerModes.Info) PrintOnConsole(sender, message);
    }

    public void Error(object sender, string message)
    {
        if(_loggerMode >= LoggerModes.Error) PrintOnConsole(sender, message);
    }

    private void PrintOnConsole(object sender, string message)
    {
        print($"{sender.GetType().Name} | {message}");
    }
}
