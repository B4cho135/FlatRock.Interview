using Application.Commands.VideoRecordings;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecordingsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public RecordingsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadVideoRecording(PostVideoRecording command)
        {
            if (command.FileBytes.Any())
            {
                await _mediator.Send(command);

                return NoContent();
            }

            return BadRequest("File empty");
        }
    }
}
