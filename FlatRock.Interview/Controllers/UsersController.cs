using Application.Commands.Users;
using Application.Models;
using Application.Models.SearchQueries;
using Application.Queries.Users;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]UsersSearchQuery query)
        {
            var result = await _mediator.Send(new GetUsersQuery() { SearchQuery = query });

            return Ok(result.Item);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _mediator.Send(new GetUserQuery() { Id = id });

            if(result.Status == ResponseStatuses.Success.ToString())
            {
                return Ok(result.Item);
            }

            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Post(RegisterUserCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, PutUserCommand command)
        {
            command.Id = id;

            var result = await _mediator.Send(command);

            if(result.Status != ResponseStatuses.Success.ToString())
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _mediator.Send(new DeleteUseCommand() { Id = id });

            if(result.Status == ResponseStatuses.Success.ToString())
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
