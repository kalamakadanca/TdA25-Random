using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api")]
public class HelloController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new { organization = "Student Cyber Games" });
    }
}
