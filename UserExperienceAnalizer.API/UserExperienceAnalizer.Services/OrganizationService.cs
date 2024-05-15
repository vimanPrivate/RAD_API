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
            TimeSpan t2 = d2 - d;

            TimeSpan ss;
            TimeSpan t3 = timeDifference + t2;

            //TimeSpan t4 = DivideTimeSpan(t3, 3);
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
                        if (!finalGoal.GoalName.Contains(item.Value.ScreenName))
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

            foreach (var item in organizationData)
            {
                if (Convert.ToDateTime(item.Value.CreatedDateTime).ToString("yyyy-MM-dd") == DateTime.Now.ToString("yyyy-MM-dd")
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

            for (int x = 0; x <= 6; x++)
                dateList.Add(DateTime.Now.AddDays(-x).ToString("yyyy-MM-dd"));

            foreach (string date in dateList)
            {
                int yCordinatesVal = 0;
                var cordinates = new GraphCordinates();

                foreach (var item in organizationData)
                {
                    string createdDate = Convert.ToDateTime(item.Value.CreatedDateTime).ToString("yyyy-MM-dd");

                    if (createdDate == date && item.Value.ScreenName == "Init")
                        yCordinatesVal++;
                }

                cordinates.x_Cordinates = date;
                cordinates.y_Cordinates = yCordinatesVal;

                model.Cordinates.Add(cordinates);
            }

            return model;
        }

        public List<FinalGoalAverageTime> GetAverageTimeToFinalGoal()
        {
            var list = new List<FinalGoalAverageTime>();

            var timeTaskDictionary = GetAverageTimeToGoal();
            var result = GetAverageTimeToGoal(timeTaskDictionary);

            foreach (var itm in result)
            {
                var model = new FinalGoalAverageTime();

                model.GoalName = itm.GoalName;
                model.AverageTimeToReach = itm.AverageTimeToTarget.TotalSeconds;

                list.Add(model);
            }

            return list;
        }

        private List<GoalViewModel> GetAverageTimeToGoal(Dictionary<Guid, Dictionary<string, TimeSpan>> list)
        {
            // var result = new Dictionary<string, TimeSpan>();
            var result = new List<GoalViewModel>();

            var finalGoalList = GetFinalGoals().GoalName;

            foreach(var goal in finalGoalList)
            {
                TimeSpan totalTimeForGoal = new TimeSpan(0, 0, 0);
                int goalCount = 0;

                foreach(var itm in list)
                {
                    var key = itm.Key;
                    var value = itm.Value;

                    string dGoal = "";
                    TimeSpan dTimeSpan = new TimeSpan(0,0,0);

                    var enumerator = value.GetEnumerator();
                    while (enumerator.MoveNext())
                    {
                        var kvp = enumerator.Current;
                        dGoal = kvp.Key;
                        dTimeSpan = kvp.Value;
                    }

                    if(goal == dGoal)
                    {
                        totalTimeForGoal = totalTimeForGoal + dTimeSpan;
                        goalCount++;
                    }
                }

                result.Add(new GoalViewModel
                {
                    GoalName = goal,
                    TotalTimeToTarget = totalTimeForGoal,
                    AverageTimeToTarget = DivideTimeSpan(totalTimeForGoal, goalCount)
                }) ;
            }

            return result;
        }

        private static TimeSpan DivideTimeSpan(TimeSpan timeSpan, int divisor)
        {
            // Convert TimeSpan to ticks, divide by the divisor, and convert back to TimeSpan
            if (divisor == 0)
                return new TimeSpan(0, 0, 0);

            long ticks = timeSpan.Ticks / divisor;
            return TimeSpan.FromTicks(ticks);
        }

        private Dictionary<Guid, Dictionary<string, TimeSpan>> GetAverageTimeToGoal()
        {
            var finalGoalList = GetFinalGoals();
            var uniqueIdList = new List<Guid>();

            // Filtering UserIDs

            foreach (var item in organizationData)
            {
                Guid id = item.Value.Id;
                if (!uniqueIdList.Contains(id))
                    uniqueIdList.Add(id);
            }

            // Calculating Each time for Final goals
            var mainDictionary = new Dictionary<Guid, Dictionary<string, TimeSpan>>();

            foreach (var id in uniqueIdList)
            {
                TimeSpan totalTimeForGoal = new TimeSpan(0, 0, 0, 0);
                var timeForEachGoal = new Dictionary<string, TimeSpan>();
                string goal = null;

                foreach (var item in organizationData)
                {
                    if (id == item.Value.Id)
                    {
                        try
                        {
                            DateTime dStart = Convert.ToDateTime(item.Value.StartDate + " " + item.Value.StartTime);
                            DateTime dEnd = Convert.ToDateTime(item.Value.EndDate + " " + item.Value.EndTime);
                            TimeSpan tmpTimeSpan = dEnd - dStart;
                            totalTimeForGoal = totalTimeForGoal + tmpTimeSpan;

                            if (!item.Value.IsFinalGoal)
                                goal = item.Value.ScreenName;
                        }
                        catch (Exception)
                        {
                            continue;
                        }
                    }
                }

                timeForEachGoal.Add(String.IsNullOrEmpty(goal) ? finalGoalList.GoalName[0] : goal, totalTimeForGoal);

                mainDictionary.Add(id, timeForEachGoal);
            }

            return mainDictionary;
        }
    }
}
