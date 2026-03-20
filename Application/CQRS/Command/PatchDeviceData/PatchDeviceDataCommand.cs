using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace Application.CQRS.Command.PatchDeviceData
{
    public class PatchDeviceDataCommand : IRequest<DeviceData>
    {
        [FromRoute(Name = "id")]
        public string? DeviceId { get;  set; }

        [JsonPropertyName("state")]
        public string State { get;  set; }

        public bool IsValid()
        {
            bool valid = !string.IsNullOrEmpty(DeviceId);

            valid = State != Parameters.Available && State != Parameters.InUse && State != Parameters.Inactive ? false : valid;

            return valid;
        }
    }
}
