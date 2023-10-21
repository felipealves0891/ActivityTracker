using ActivityTracker.Core;

namespace ActivityTracker.Api.Services.RepositoryService;

public interface IRepository : IDisposable
{
    Task<IEnumerable<ActivityEntity>> GetActivitiesAsync();

    Task<ActivityEntity> GetActivityByIdAsync(int id);

    Task<ActivityEntity> GetActivityLastetAsync();
}
