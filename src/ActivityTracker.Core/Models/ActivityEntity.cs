namespace ActivityTracker.Core;

public class ActivityEntity
{
    public int Id { get; private set; }

    public string Uuid { get; private set;} = string.Empty;

    public string MachineName { get; private set;} = string.Empty;

    public string ProcessName { get; private set;} = string.Empty;

    public string WindowTitle { get; private set;} = string.Empty;

    public DateTime Start { get; private set;}

    public DateTime End { get; private set;}

    protected ActivityEntity()
    {
        // For EF
    }
}
