using System.Collections.Generic;

namespace UserExperienceAnalizer.API.Models
{
    public class OrganizationInfoModel
    {
        public GeneralInfo General { get; set; }
        public FinalGoals FinalGoals { get; set; }
    }

    public class FinalGoals
    {
        public List<string> GoalName { get; set; }
    }

    public class GeneralInfo
    {
        public int ApplicationUsageCount { get; set; }
    }
}
