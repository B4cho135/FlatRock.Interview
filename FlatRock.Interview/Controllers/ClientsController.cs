using Application.Commands.Clients;
using Application.Queries.Clients;
using Application.Services;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ClientsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetClientsQuery());

            return Ok(result?.Item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _mediator.Send(new GetClientQuery() { Id = id }));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody] PostClientCommand? command)
        {
            await _mediator.Send(command);

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,[FromBody] PutClientCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteClientCommand(id));

            if(result.Status == ResponseStatuses.Success.ToString())
            {
                return NoContent();
            }

            return BadRequest(result);
        }
    }
}
