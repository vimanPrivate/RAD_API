using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using UserExperienceAnalizer.Common.Models;
using UserExperienceAnalizer.Common.Validation;
using UserExperienceAnalizer.Service.Services;

namespace UserExperienceAnalizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ApplicationController : ControllerBase
    {
        private readonly GlobalVar _globalVar;
        private ApplicationService applicationService;

        public ApplicationController(GlobalVar globalVar)
        {
            this.applicationService = new ApplicationService();
            _globalVar = globalVar;
        }

        
        [HttpPost]
        [Route("InitRequest")]
        public IActionResult InitRequest([FromBody] KeyStrokeModel request)
        {

            try
            {
                _globalVar.UserID = Guid.NewGuid();

                InputValidation.ValidateOraganization(request.OrganizationName, "OrganizationName Cannot be empty");
                _globalVar.Organization = request.OrganizationName;

                return Ok(new
                {
                    Message = "Success"
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Message = e.Message
                });
            }
        }

        [HttpPost]
        [Route("CaptureKeyStorokes")]
        public IActionResult CaptureKeyStorokes([FromBody] KeyStrokeModel request)
        {

            request.Id = _globalVar.UserID;
            request.OrganizationName = _globalVar.Organization;

            try
            {
                applicationService.CaptureKeyStorokes(request);
                var msg = new
                {
                    Message = "Success!"
                };

                return Ok(msg);
            }
            catch(Exception ex)
            {
                var msg = new
                {
                    Message = ex.Message
                };

                return BadRequest(msg);
            }
        }
    }
}
