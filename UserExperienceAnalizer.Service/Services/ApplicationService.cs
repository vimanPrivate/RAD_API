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
            InputValidation.ValidateStartDate(request.StartTime);
            InputValidation.ValidateEndDate(request.EndTime);
            InputValidation.ValidateOraganization(request.OrganizationName);
            InputValidation.ValidateScreenName(request.ScreenName);

            var client = firebase.InitFirebaseClient();
            var response = client.Push(request.OrganizationName+"/", request);
        }
    }
}
