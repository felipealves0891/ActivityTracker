using ActivityTracker.Api.Data;
using ActivityTracker.Api.Models;
using ActivityTracker.Api.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace ActivityTracker.Api.Services.ActivityUpdaterService
{
    public class ActivityUpdater : IActivityUpdater
    {
        private readonly ApplicationDbContext _context;
        private Activity? _current;

        public ActivityUpdater(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task UpdateAsync(UpdateActivityDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
                return;

            if (_current is null)
                _current = (await GetLastActivity()) ?? new Activity(dto.Name, dto.Created);

            _current = _current.UpdateOrNow(dto);

            if(_current.Id == 0)
                await _context.Activities.AddAsync(_current);
            else
                _context.Activities.Update(_current);

            await _context.SaveChangesAsync();
        }

        private async Task<Activity?> GetLastActivity()
        {
            return await _context.Activities
                                 .OrderByDescending(x => x.EndAt)
                                 .ThenByDescending(x => x.StartAt)
                                 .FirstOrDefaultAsync();
        }
    }
}
