using Application.CQRS.Command.DeleteDeviceData;
using Application.CQRS.Command.PatchDeviceData;
using Application.CQRS.Command.PostDeviceData;
using Application.CQRS.Command.PutDeviceData;
using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DeviceSystemDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceSystemDataAPIController : ControllerBase
    {
        public DeviceSystemDataAPIController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private readonly IMediator _mediator;


        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetPagedDeviceDataQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] GetDeviceDataQuery query)
        {
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDeviceDataCommand command)
        {
            await _mediator.Send(command);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] PutDeviceDataCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] PatchDeviceDataCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Patch(string id)
        {
            var command = new DeleteDeviceDataCommand { DeviceId = id.ToString() };

            await _mediator.Send(command);

            return Ok();
        }
    }
}
