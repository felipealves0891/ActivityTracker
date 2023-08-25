namespace ActivityTracker.Core.Models;

public class Activity
{
    public Activity(
        string machineName, 
        string processName, 
        string windowTitle, 
        DateTime startTime, 
        bool isActive)
    {
        MachineName = machineName;
        ProcessName = processName;
        WindowTitle = windowTitle;
        StartTime = startTime;
        ProcessTime = DateTime.UtcNow;
        IsActive = isActive;
    }

    public string MachineName { get; private set; }

    public string ProcessName { get; private set; }

    public string WindowTitle { get; private set; }

    public DateTime StartTime { get; private set; }

    public DateTime ProcessTime { get; private set; }

    public bool IsActive { get; private set; }

}
