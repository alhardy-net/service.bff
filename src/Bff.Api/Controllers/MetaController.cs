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
            try
            {
                throw new UnauthorizedAccessException();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "error occurred");
                throw;
            }
        }
    }
}