using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ActivityTracker.Models
{
    public class Activity
    {
        public Activity(string name)
        {
            Id = Guid.NewGuid().ToString("N");
            Name = name;
            Created = DateTime.UtcNow;
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public DateTime Created { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<Activity>(this);
        }

    }
}
