using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API
{
    public class KeyStrokeModel
    {
        public string StartTime { get; set; }
        public string StartDate { get; set; }
        public string EndTime { get; set; }
        public string EndDate { get; set; }
        public string ScreenName { get; set; }
        public string ButtonName { get; set; } 
        public int Sequence { get; set; }
        public string Property1 { get; set; }
        public string Property2 { get; set; }
        public bool IsFinalGoal { get; set; }
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
        public string CreatedDateTime { get; set; }
    }
}
