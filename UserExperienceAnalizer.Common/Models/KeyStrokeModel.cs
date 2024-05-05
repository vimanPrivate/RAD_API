using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace UserExperienceAnalizer.Common.Models
{
    public class KeyStrokeModel
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string ScreenName { get; set; }
        public bool IsFinalGoal { get; set; }
        public Guid Id { get; set; }
        public string OrganizationName { get; set; }
    }
}
