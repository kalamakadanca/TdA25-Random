using Microsoft.AspNetCore.Mvc;

namespace aspnetapp.Controllers
{
    public class ApiController : Controller
    {
        [HttpGet]
        [Route("api")]
        public IActionResult Api()
        {
            return Json(new Secret());
        }
    }

    public class Secret
    {
        public string organization { get; set; } = "Student Cyber Game";
    }
}
