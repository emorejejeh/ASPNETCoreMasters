using DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ASPNetCoreMastersTodoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly Authentication _authentication;

        public UsersController(IOptions<Authentication> authentication)
        {
            _authentication = authentication.Value;
        }

        [HttpGet()]
        public IActionResult Login()
        {
            return Ok(_authentication);
        }
    }
}
