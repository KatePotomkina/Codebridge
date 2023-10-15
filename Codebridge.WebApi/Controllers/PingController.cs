using Codebridge.Service;
using Microsoft.AspNetCore.Mvc;

namespace Codebridge.Controllers;

[ApiController]
[Route("[controller]")]
public class PingController:ControllerBase
{
    private readonly IPingService _pingService;

    public PingController(IPingService pingService)
    {
        _pingService = pingService;
    }

    [HttpGet]
    public string Get() => _pingService.Ping();
}
