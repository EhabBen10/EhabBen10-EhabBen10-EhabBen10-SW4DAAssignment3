using Microsoft.AspNetCore.Mvc;
using SW4DAAssignment3.Models;
using SW4DAAssignment3.Services;

namespace SW4DAAssignment3.Controllers;

[Route("[controller]")]
[ApiController]
public class LoggingControler : ControllerBase
{
    private readonly LogService _logService;

    public LoggingControler(LogService logService) => _logService = logService;

    [HttpGet]
    public async Task<List<Binding>> Get(string specificUser, string operation)
    {
        return await _logService.GetLogs(specificUser, operation);
    }

}