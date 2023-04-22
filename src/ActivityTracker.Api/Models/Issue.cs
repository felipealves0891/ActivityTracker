using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ActivityTracker.Api.Models
{
    public class Issue
    {
        public int Id { get; private set; }

        public string Name { get; private set; } = string.Empty;

        public string Description { get; private set; } = string.Empty;

        public DateTime CreatedDate { get; private set; }

        public DateTime DueDate { get; private set; }

        public DateTime? UpdatedDate { get; private set; }

        public DateTime? CloseDate { get; private set; }
        
        public IList<Activity> Activities { get; private set; } = new List<Activity>();

        private Issue()
        {
            // For EF
        }

        public Issue(string name, string description, DateTime createdDate, DateTime dueDate)
        {
            Name = name;
            Description = description;
            CreatedDate = createdDate;
            DueDate = dueDate;
        }

        public void AddActivity(Activity activity)
        {
            activity.SetIssue(this);
            Activities.Add(activity);
        }

    }
}
