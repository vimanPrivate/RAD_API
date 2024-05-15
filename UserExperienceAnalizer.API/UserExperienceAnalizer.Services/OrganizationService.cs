using System.Collections.Generic;
using System;
using UserExperienceAnalizer.API.Models;
using System.Data;
using Newtonsoft.Json;
using System.Collections;

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
            //string json = JsonConvert.SerializeObject(organizationData);

            var d = Convert.ToDateTime("2024-05-10 19:10:27");
            var d2 = Convert.ToDateTime("2024-05-10 19:10:45");

            TimeSpan timeDifference = d2 - d;
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

        public FinalGoals GetFinalGoals()
        {
            try
            {
                var finalGoal = new FinalGoals();
                finalGoal.GoalName = new List<string>();

                foreach (var item in organizationData)
                {
                    if (item.Value.IsFinalGoal)
                    {
                        if(!finalGoal.GoalName.Contains(item.Value.ScreenName))
                            finalGoal.GoalName.Add(item.Value.ScreenName);
                    }                     
                }

                return finalGoal;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public GeneralInfo GetGeneralInfo()
        {
            var model = new GeneralInfo();

            model.TodayApplicationUsageCount = GetTodayLoggedInCount();
           
            return model;
        }

        private int GetTodayLoggedInCount()
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

        public GraphInfo GetGraphInfo()
        {
            var model = new GraphInfo();
            model.DailyAppUsage = new DailyAppUsageGraph();
            model.DailyAppUsage = GetDailyAppUsageGraph();

            return model;
        }

        private DailyAppUsageGraph GetDailyAppUsageGraph() 
        {
            var model = new DailyAppUsageGraph();
            model.Cordinates = new List<GraphCordinates>();

            var dateList = new List<string>();

            for(int x = 0; x <= 6; x++)
                dateList.Add(DateTime.Now.AddDays(-x).ToString("yyyy-MM-dd"));

            foreach(string date in dateList)
            {
                int yCordinatesVal = 0;
                var cordinates = new GraphCordinates();

                foreach (var item in organizationData)
                {
                    string createdDate = Convert.ToDateTime(item.Value.CreatedDateTime).ToString("yyyy-MM-dd");
                   
                    if(createdDate == date && item.Value.ScreenName == "Init")
                        yCordinatesVal++;
                }

                cordinates.x_Cordinates = date;
                cordinates.y_Cordinates = yCordinatesVal;

                model.Cordinates.Add(cordinates);
            }
            
            return model;
        }

        public void GetAverageTimeToTaskGraphData()
        {
            var finalGoalList = GetFinalGoals();
            var uniqueIdList = new List<Guid>();

            foreach(var item in organizationData)
            {
                Guid id = item.Value.Id;
                if(!uniqueIdList.Contains(id))
                    uniqueIdList.Add(id);
            }

            

            foreach(var id in uniqueIdList)
            {
                foreach(var item in organizationData)
                {
                    if(id == item.Value.Id)
                    {
                        if (!item.Value.IsFinalGoal)
                        {
                            // calculate the time 
                        }
                    }
                }
            }
        }
    }
}
