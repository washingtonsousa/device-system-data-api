using Application.CQRS.Command.DeleteDeviceData;
using Application.CQRS.Command.PatchDeviceData;
using Application.CQRS.Command.PostDeviceData;
using Application.CQRS.Command.PutDeviceData;
using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DeviceSystemDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceDataController : ControllerBase
    {
        public DeviceDataController(IMediator mediator)
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
            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PostDeviceDataCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute]string id, [FromBody] PutDeviceDataCommand command)
        {
            command.DeviceId = id;

            return Ok(await _mediator.Send(command));
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(string id, [FromBody] PatchDeviceDataCommand command)
        {
            command.DeviceId = id;

            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var command = new DeleteDeviceDataCommand { DeviceId = id.ToString() };

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
