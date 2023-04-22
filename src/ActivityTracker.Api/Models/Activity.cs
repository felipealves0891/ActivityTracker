using ActivityTracker.Api.Models.Dtos;

namespace ActivityTracker.Api.Models
{
    public class Activity
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public DateTime StartAt { get; private set; }

        public DateTime? EndAt { get; private set; }

        public int? IssueId { get; private set; }

        public Issue? Issue { get; private set; }

        private Activity() 
        {
            // For EF
        }

        public Activity(string name, DateTime startAt)
        {
            Name = name;
            StartAt = startAt;
        }

        public Activity UpdateOrNow(UpdateActivityDto dto)
        {
            if (!Name.Equals(dto.Name))
                return new Activity(dto.Name, dto.Created);

            EndAt = dto.Created;
            return this;
        }

        public void SetIssue(Issue issue)
        {
            IssueId = issue.Id;
            Issue = issue;
        }
    }
}
