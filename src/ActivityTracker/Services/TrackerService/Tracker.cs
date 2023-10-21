using ActivityTracker.Core.Models;
using System.Runtime.InteropServices;

namespace ActivityTracker.Services.TrackerService
{
    public class Tracker : ITrackerService
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();
        
        private readonly ILogger<Tracker> _logger;

        public Tracker(ILogger<Tracker> logger)
        {
            _logger = logger;
        }

        public IEnumerable<ProcessEntity> GetActivities()
        {
            try
            {
                var processes = System.Diagnostics.Process.GetProcesses();
                return processes.Where(process => process.MainWindowHandle != IntPtr.Zero)
                                .Select(Create)
                                .ToArray();    
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Tracker error: ");
                return Enumerable.Empty<ProcessEntity>();
            }
            
        }

        private ProcessEntity Create(System.Diagnostics.Process process)
        {
            var isActive = process.MainWindowHandle == GetForegroundWindow();
            return new ProcessEntity(
                process.MachineName, 
                process.ProcessName, 
                process.MainWindowTitle, 
                process.StartTime, 
                isActive);
        }

    }
}
