using FireSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserExperienceAnalizer.API.Models;

namespace UserExperienceAnalizer.API
{
    public class ApplicationService
    {
        private FirebaseClient firebase;
        public ApplicationService()
        {
            this.firebase = new FirebaseClient();
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


            return null;
        }
    }
}
