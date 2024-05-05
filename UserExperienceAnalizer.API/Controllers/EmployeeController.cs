using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UserExperienceAnalizer.API.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Test()
        {

            return null;
        }
    }
}
