using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserExperienceAnalizer.API.Models
{
    public class HitCountViewModel
    {
        public string GoalName { get; set; }
        public int HitCount { get; set; }
    }

    public class HitCountMainViewModel
    {
        public string Date { get; set; }
        public List<HitCountViewModel> HitCountList { get; set; }
    }
}
