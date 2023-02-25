using System;
using System.Text;

namespace TDDMicroExercises.TelemetrySystem;

public class EventSimulator : IEventsSimulator
{
    private readonly Random _connectionEventsSimulator = new Random(42);

    public bool CanConnect() => _connectionEventsSimulator.Next(1, 10) <= 8;

    public string Message()
    {
        var message = new StringBuilder();
        var lenght = _connectionEventsSimulator.Next(50, 110);
        for (var i = lenght; i >= 0; --i)
        {
            message.Append((char)_connectionEventsSimulator.Next(40, 126));
        }

        return message.ToString();
    }
}