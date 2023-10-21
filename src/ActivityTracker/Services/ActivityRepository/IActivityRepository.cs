using ActivityTracker.Core.Models;
using System.Collections.Generic;

namespace ActivityTracker.Services.ActivityRepository
{
    public interface IActivityRepository
    {
        Task SaveAsync(IEnumerable<ProcessEntity> activities, CancellationToken cancellationToken);
    }
}
