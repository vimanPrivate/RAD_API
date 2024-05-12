using System.Collections.Generic;
using System;
using UserExperienceAnalizer.API.Models;
using System.Data;

namespace UserExperienceAnalizer.API.UserExperienceAnalizer.Services
{
    public class OrganizationService
    {
        private FirebaseClient firebase;
        Dictionary<string, KeyStrokeModel> organizationData;

        public OrganizationService(string organizationName)
        {
            firebase = new FirebaseClient();
            organizationData = new Dictionary<string, KeyStrokeModel>();

            organizationData = GetOrganizationInfo(organizationName);
        }

        public Dictionary<string, KeyStrokeModel> GetOrganizationInfo(string organization)
        {
            try
            {
                var client = firebase.InitFirebaseClient();
                var response = client.Get(organization + "/");

                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, KeyStrokeModel>>();

                    return data;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public FinalGoals GetFinalGoals(string organizationName)
        {
            try
            {
                var finalGoal = new FinalGoals();
                finalGoal.GoalName = new List<string>();

                foreach (var item in organizationData)
                {
                    if (item.Value.IsFinalGoal)
                        finalGoal.GoalName.Add(item.Value.ScreenName);
                }

                return finalGoal;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public GeneralInfo GetGeneralInfo(string organizationName)
        {
            var model = new GeneralInfo();

            model.TodayApplicationUsageCount = GetTodayLoggedInCount(organizationName);
           

            return model;
        }

        private int GetTodayLoggedInCount(string organizationName)
        {
            int count = 0;

            foreach(var item in organizationData)
            {
                if(Convert.ToDateTime(item.Value.CreatedDateTime).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd") 
                    && item.Value.ScreenName == "Init")
                    count++;
            }

            return count;
        }
    }
}
