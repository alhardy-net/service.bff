using Microsoft.AspNetCore.Mvc;

namespace Bff.Api.Controllers
{
    [Microsoft.AspNetCore.Components.Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class MetaController : ControllerBase
    {
        [HttpGet("ping")]
        public ActionResult<string> Ping()
        {
            return Ok("pong2");
        }
    }
}