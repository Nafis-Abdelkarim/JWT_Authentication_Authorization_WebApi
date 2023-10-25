using Microsoft.AspNetCore.Mvc;
using JWT_Auth_WebApi.Models;
namespace JWT_Auth_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IAppAuthService _appAuthService;
        public UsersController(IAppAuthService appAuthService)
        {
            _appAuthService = appAuthService;
        }

        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate(User user)
        {
            var token = await _appAuthService.Authentification(user);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
