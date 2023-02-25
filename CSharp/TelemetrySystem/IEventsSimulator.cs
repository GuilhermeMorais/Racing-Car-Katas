namespace TDDMicroExercises.TelemetrySystem;

public interface IEventsSimulator
{
    bool CanConnect();
    string Message();
}