using ActivityTracker.Core.Models;
using System.Runtime.InteropServices;

namespace ActivityTracker.Services.TrackerService
{
    public class Tracker : ITrackerService
    {
        private IntPtr _handleForegroundWindow;

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static extern IntPtr GetForegroundWindow();

        public IEnumerable<Activity> GetActivities()
        {
            var _handleForegroundWindow = GetForegroundWindow();
            var processes = System.Diagnostics.Process.GetProcesses();
            return processes.Where(process => process.MainWindowHandle != IntPtr.Zero)
                            .Select(Create)
                            .ToArray();
        }

        private Activity Create(System.Diagnostics.Process process)
        {
            return new Activity(
                process.MachineName, 
                process.ProcessName, 
                process.MainWindowTitle, 
                process.StartTime, 
                process.MainWindowHandle.Equals( _handleForegroundWindow));
        }

    }
}
