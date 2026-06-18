using Microsoft.AspNetCore.Mvc;

namespace AcmeClinic.Api.Controllers;

[ApiController]
[Route("api/health")]
public class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            message = "API funcionando"
        });
    }
}