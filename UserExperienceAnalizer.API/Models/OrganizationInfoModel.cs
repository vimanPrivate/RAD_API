using System;
using System.Collections.Generic;

namespace UserExperienceAnalizer.API.Models
{
    public class OrganizationInfoModel
    {
        public GeneralInfo General { get; set; }
        public FinalGoals FinalGoals { get; set; }
        public GraphInfo Graph { get; set; }
    }

    public class FinalGoals
    {
        public List<string> GoalName { get; set; }
        public List<FinalGoalAverageTime> GoalReachingTime { get; set; }
    }

    public class GeneralInfo
    {
        public int TodayApplicationUsageCount { get; set; }
    }

    public class FinalGoalAverageTime
    {
        public string GoalName { get; set; }
        public double AverageTimeToReach { get; set; }
    }

    public class GraphInfo
    {
        public DailyAppUsageGraph DailyAppUsage { get; set; }
    }

    public class DailyAppUsageGraph
    {
        public List<GraphCordinates> Cordinates { get; set; }
    }

    public class GraphCordinates
    {
        public string x_Cordinates { get; set; }
        public int y_Cordinates { get; set; }
    }
}
