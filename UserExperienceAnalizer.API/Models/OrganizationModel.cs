using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace UserExperienceAnalizer.API.Models
{
    public class OrganizationModel
    {
        public List<string> OrganizationNames { get; set; }
    }
}
