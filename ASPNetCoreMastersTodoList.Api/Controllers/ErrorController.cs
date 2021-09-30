using Microsoft.AspNetCore.Mvc;

namespace ASPNetCoreMastersTodoList.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [HttpGet()]
        public IActionResult Error() => Problem();
    }
}
