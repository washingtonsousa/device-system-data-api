using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace Application.CQRS.Command.PatchDeviceData
{
    public class PatchDeviceDataCommand : IRequest<DeviceData>
    {
        [FromRoute(Name = "id")]
        public string? DeviceId { get; set; }

        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("brand")]
        public string? Brand { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(DeviceId))
                return false;

            if (Name == null && Brand == null && State == null)
                return false;

            if (State != null && State != Parameters.Available && State != Parameters.InUse && State != Parameters.Inactive)
                return false;

            return true;
        }
    }
}
