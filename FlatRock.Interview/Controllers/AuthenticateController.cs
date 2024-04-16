using Application.Commands.Authenticate;
using Application.Commands.Users;
using Application.Services;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticateController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;

        public AuthenticateController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,
            IMediator mediator)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _mediator = mediator;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var result = await _mediator.Send(command);

            return result.Match<IActionResult>(success => Ok(success), error => Unauthorized(error));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var result = await _mediator.Send(command);

            if(result.Status == ResponseStatuses.Success.ToString())
            {
                return Ok(result.Item);
            }

            return BadRequest(result.Message);
        }
    }
}
