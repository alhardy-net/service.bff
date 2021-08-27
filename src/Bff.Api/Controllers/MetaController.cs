using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bff.Api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class MetaController : ControllerBase
    {
        private readonly ILogger<MetaController> _logger;

        public MetaController(ILogger<MetaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("pong");
        }

        [HttpGet("error")]
        public ActionResult Error()
        {
            throw new UnauthorizedAccessException();
        }
        
        [HttpGet("warn")]
        public ActionResult Warn()
        {
            _logger.LogWarning("Warning message...");
            
            return Ok();
        }
    }
}