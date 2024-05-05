using System;
using System.Collections.Generic;
using System.Text;
using UserExperienceAnalizer.Common.Models;

namespace UserExperienceAnalizer.Common.Validation
{
    public class InputValidation
    {
        public static void ValidateStartDate(string date)
        {
            if (String.IsNullOrEmpty(date.ToString()))
                throw new Exception("Start Date Cannot be empty");
        }

        public static void ValidateEndDate(string date)
        {
            if (String.IsNullOrEmpty(date.ToString()))
                throw new Exception("End Date Cannot be empty");
        }

        public static void ValidateOraganization(string organization)
        {
            if (String.IsNullOrEmpty(organization))
                throw new Exception("Organization Cannot be empty");
        }

        public static void ValidateScreenName(string screenName)
        {
            if (String.IsNullOrEmpty(screenName))
                throw new Exception("ScreenName Cannot be empty");
        }
    }
}
