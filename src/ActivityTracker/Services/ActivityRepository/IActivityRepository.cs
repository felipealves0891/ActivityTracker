using ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Services.ActivityRepository
{
    public interface IActivityRepository
    {
        Task SaveAsync(Activity activity, CancellationToken cancellationToken);
    }
}
