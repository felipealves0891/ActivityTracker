using ActivityTracker.Api.Models;
using ActivityTracker.Api.Models.Dtos;

namespace ActivityTracker.Api.Services.ActivityUpdaterService
{
    public interface IActivityUpdater
    {
        Task UpdateAsync(UpdateActivityDto dto);
    }
}
