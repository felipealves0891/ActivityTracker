using ActivityTracker.Services.ActivityRepository;
using ActivityTracker.Services.TrackerService;

namespace ActivityTracker
{
    public class Worker : BackgroundService
    {
        private readonly ITrackerService _tracker;
        private readonly IActivityRepository _repository;
        private readonly ILogger<Worker> _logger;

        public Worker(ITrackerService tracker, IActivityRepository repository, ILogger<Worker> logger)
        {
            _tracker = tracker;
            _repository = repository;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    var activity = _tracker.GetActivities();
                    if (!activity.Any())
                        continue;
                        
                    await _repository.SaveAsync(activity, stoppingToken);
                    await Task.Delay(100);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: ");
                Console.ReadKey();
            }
        }
    }
}