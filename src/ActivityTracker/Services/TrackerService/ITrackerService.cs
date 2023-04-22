using ActivityTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActivityTracker.Services.TrackerService
{
    public interface ITrackerService
    {
        Activity GetActivity();
    }
}
