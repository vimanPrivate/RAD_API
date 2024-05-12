using System.Collections.Generic;
using System;
using UserExperienceAnalizer.API.Models;
using System.Data;

namespace UserExperienceAnalizer.API.UserExperienceAnalizer.Services
{
    public class OrganizationService
    {
        private FirebaseClient firebase;

        public OrganizationService()
        {
            firebase = new FirebaseClient();
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
                var data = GetOrganizationInfo(organizationName);

                var finalGoal = new FinalGoals();
                finalGoal.GoalName = new List<string>();

                foreach (var item in data)
                {
                    if (item.Value.IsFinalGoal)
                        finalGoal.GoalName.Add(item.Value.ScreenName);
                }

                return finalGoal;

                //var client = firebase.InitFirebaseClient();
                //var response = client.Get(organizationName+"/");

                //if (response.Body != "null")
                //{
                //    var list = new List<string>();
                //    var data = response.ResultAs<Dictionary<string, KeyStrokeModel>>();

                //    var finalGoal = new FinalGoals();
                //    finalGoal.GoalName = new List<string>();

                //    foreach (var item in data)
                //    {
                //        if(item.Value.IsFinalGoal)
                //            finalGoal.GoalName.Add(item.Value.ScreenName);
                //    }

                //    return finalGoal;
                //}
                //else
                //    return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
