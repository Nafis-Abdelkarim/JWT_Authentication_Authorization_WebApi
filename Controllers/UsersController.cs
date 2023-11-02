using Microsoft.AspNetCore.Mvc;
using JWT_Auth_WebApi.Models;
using Microsoft.AspNetCore.Authorization;

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

        //this is an example of an public endpoint 
        [HttpGet]
        [Route("Home")]
        public string Get()
        {
            return "Hello World";
        }

        //Also the authentification is a public endpoint
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginUser loginUser)
        {
            var token = await _appAuthService.Authentification(loginUser);
            if(token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("Dashboard")]
        public string GetDashbord()
        {
            return "You are in the Dashboard";
        }
    }
}
