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
using UserExperienceAnalizer.Service.Services;

namespace UserExperienceAnalizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            _globalVar.UserID = Guid.NewGuid();
            _globalVar.Organization = request.OrganizationName;

            return Ok("Sucess");
        }

        [HttpPost]
        [Route("CaptureKeyStorokes")]
        public IActionResult CaptureKeyStorokes([FromBody] KeyStrokeModel request)
        {

            request.Id = _globalVar.UserID;
            request.OrganizationName = _globalVar.Organization;

            applicationService.CaptureKeyStorokes(request);
            return Ok(request);
        }
    }
}
