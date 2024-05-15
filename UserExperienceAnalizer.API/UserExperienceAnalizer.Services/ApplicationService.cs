using FireSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserExperienceAnalizer.API.Models;
using UserExperienceAnalizer.API.UserExperienceAnalizer.Common;

namespace UserExperienceAnalizer.API.UserExperienceAnalizer.Services
{
    public class ApplicationService
    {
        private FirebaseClient firebase;

        public ApplicationService()
        {
            firebase = new FirebaseClient();
        }

        public bool InitRequest(string organizationName, Guid id)
        {
            var model = new KeyStrokeModel();

            model.Id = id;
            model.ScreenName = "Init";
            model.CreatedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            model.OrganizationName = organizationName;

            var client = firebase.InitFirebaseClient();
            var response = client.Push(organizationName + "/", model);

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Logs.InfoLog("Session Started!\nID : "+model.Id+"\nOrganization : "+model.OrganizationName);
                return true;
            }             

            return false;
        }

        public void CaptureKeyStorokes(KeyStrokeModel request)
        {
            InputValidation.ValidateOraganization(request.OrganizationName);
            InputValidation.ValidateStartDate(request.StartDate);
            InputValidation.ValidateEndDate(request.EndDate);
            InputValidation.ValidateStartTime(request.StartTime);
            InputValidation.ValidateEndTime(request.EndTime);
            InputValidation.ValidateScreenName(request.ScreenName);

            request.CreatedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            var client = firebase.InitFirebaseClient();
            var response = client.Push(request.OrganizationName + "/", request);

            Logs.InfoLog("Key Stroke captured\nID : "+request.Id+"\nScreenName : "+request.ScreenName);
        }

        public CommonRespond<OrganizationModel> GetOrganizations()
        {
            try
            {
                var client = firebase.InitFirebaseClient();
                var response = client.Get("/");

                if (response.Body != "null")
                {
                    var list = new List<string>();
                    var data = response.ResultAs<Dictionary<string, object>>();

                    var commonResponse = new CommonRespond<OrganizationModel>();

                    commonResponse.Response = new Response();
                    commonResponse.Response.Message = "Success!";
                    commonResponse.Data = new OrganizationModel();

                    foreach (var item in data)
                        list.Add(item.Key);

                    commonResponse.Data.OrganizationNames = list;

                    return commonResponse;
                }
                else
                    return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public CommonRespond<OrganizationInfoModel> GetOrganizationInfo(string organizationName)
        {
            OrganizationService organizationService = new OrganizationService(organizationName);


            var model = new CommonRespond<OrganizationInfoModel>();

            model.Response = new Response();
            model.Response.Message = "Success!";

            model.Data = new OrganizationInfoModel();

            model.Data.FinalGoals = new FinalGoals();
            model.Data.FinalGoals = organizationService.GetFinalGoals();

            model.Data.FinalGoals.GoalReachingTime = new List<FinalGoalAverageTime>();
            model.Data.FinalGoals.GoalReachingTime = organizationService.GetAverageTimeToFinalGoal();

            model.Data.General = new GeneralInfo();
            model.Data.General = organizationService.GetGeneralInfo();

            var graphData = organizationService.GetGraphInfo();
            model.Data.Graph = new GraphInfo();
            model.Data.Graph.DailyAppUsage = new DailyAppUsageGraph();
            model.Data.Graph.DailyAppUsage = graphData.DailyAppUsage;
            model.Data.Graph.DailyGoalHitList = new DailyGoalHitList();
            model.Data.Graph.DailyGoalHitList.DailyGoalHits = new List<DailyGoalHit>();
            model.Data.Graph.DailyGoalHitList.DailyGoalHits = organizationService.GetDailyGoalHitCount();

            organizationService.GetDailyGoalHitCount();


            return model;
        }

        public Dictionary<string, KeyStrokeModel> GetRawData(string org)
        {
            OrganizationService organizationService = new OrganizationService(org);
            var res = organizationService.GetOrganizationInfo(org);

            return res;
        }
    }
}
