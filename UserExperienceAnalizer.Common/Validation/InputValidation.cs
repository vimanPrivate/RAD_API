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
            else if (date.Length != 10)
                throw new Exception("Invalid 'Start Date'.  Expected :- 'yyyy-MM-dd'");
        }

        public static void ValidateEndDate(string date)
        {
            if (String.IsNullOrEmpty(date.ToString()))
                throw new Exception("End Date Cannot be empty");
            else if (date.Length != 10)
                throw new Exception("Invalid 'End Date'. Expected :- 'yyyy-MM-dd'");
        }

        public static void ValidateStartTime(string time)
        {
            if (String.IsNullOrEmpty(time.ToString()))
                throw new Exception("Start Time Cannot be empty");
            else if (time.Length != 8)
                throw new Exception("Invalid 'Start Time'. Expected :- 'HH:MM:ss'");
        }

        public static void ValidateEndTime(string time)
        {
            if (String.IsNullOrEmpty(time.ToString()))
                throw new Exception("End Time Cannot be empty");
            else if (time.Length != 8)
                throw new Exception("Invalid 'End Time'. Expected :- 'HH:MM:ss'");
        }

        public static void ValidateOraganization(string organization,string message = null)
        {
            if (String.IsNullOrEmpty(organization))
            {
                if (message == null)
                    throw new Exception("Organization Cannot be empty. Please Run 'api/Application/InitRequest' at the initial stage.");
                else
                    throw new Exception(message);
            }
                
        }

        public static void ValidateScreenName(string screenName)
        {
            if (String.IsNullOrEmpty(screenName))
                throw new Exception("ScreenName Cannot be empty");
        }
    }
}
