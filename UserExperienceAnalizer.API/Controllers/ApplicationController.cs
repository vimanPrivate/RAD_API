using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserExperienceAnalizer.API.Models;
using UserExperienceAnalizer.API.UserExperienceAnalizer.Common;
using UserExperienceAnalizer.API.UserExperienceAnalizer.Services;

namespace UserExperienceAnalizer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ApplicationController : ControllerBase
    {
        private readonly GlobalVar _globalVar;
        private ApplicationService applicationService;
        private ErrorRespond errorRespond;
        private TokenService token;

        public ApplicationController(GlobalVar globalVar)
        {
            this.applicationService = new ApplicationService();
            _globalVar = globalVar;
            this.errorRespond = new ErrorRespond();
            this.token = new TokenService();
        }

        [HttpGet]
        [Route("GetOrganizations")]
        public IActionResult GetOrganizations()
        {
            try
            {
                var result = applicationService.GetOrganizations();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(errorRespond.GetErrorRespond(e));
            }
        }

        [HttpGet]
        [Route("GetOrganizationInfo")]
        public IActionResult GetOrganizationInfo(string organization)
        {
            try
            {
                var result = applicationService.GetOrganizationInfo(organization);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(errorRespond.GetErrorRespond(e));
            }
        }

        [HttpGet]
        [Route("GetRawData")]
        public IActionResult GetRawData(string organization)
        {
            try
            {
                var result = applicationService.GetRawData(organization);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(errorRespond.GetErrorRespond(e));
            }
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

                bool result = applicationService.InitRequest(_globalVar.Organization, _globalVar.UserID);
                if (result)
                {
                    return Ok(new
                    {
                        Message = "Success",
                        Token = token.GetToken()
                    });
                }
                else
                {
                    return BadRequest(new
                    {
                        Message = "Coudnt connect to the Database. Please contact Admin"
                    });
                }

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
            catch (Exception ex)
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
