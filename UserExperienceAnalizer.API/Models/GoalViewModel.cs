using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API.Models
{
    public class GoalViewModel
    {
        public string GoalName { get; set; }
        public TimeSpan TotalTimeToTarget { get; set; }
        public TimeSpan AverageTimeToTarget { get; set; }
    }
}
