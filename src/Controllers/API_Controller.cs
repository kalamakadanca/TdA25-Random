using Microsoft.AspNetCore.Mvc;

namespace TourDeApp.Controllers
{
    [ApiController]
    [Route("api")]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var response = new { organization = "Student Cyber Games" };
            return Ok(response);
        }
    }
}