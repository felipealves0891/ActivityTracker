using ActivityTracker.Core.Models;
using System.Collections.Generic;

namespace ActivityTracker.Services.ActivityRepository
{
    public interface IActivityRepository
    {
        Task SaveAsync(IEnumerable<Activity> activities, CancellationToken cancellationToken);
    }
}
