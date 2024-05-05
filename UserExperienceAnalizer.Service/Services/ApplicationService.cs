using System;
using System.Collections.Generic;
using System.Text;
using UserExperienceAnalizer.Common.Models;
using UserExperienceAnalizer.Common.Validation;
using UserExperienceAnalizer.Data;

namespace UserExperienceAnalizer.Service.Services
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

            var client = firebase.InitFirebaseClient();
            var response = client.Push(request.OrganizationName+"/", request);
        }
    }
}
