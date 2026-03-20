using Domain.Constants;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.CQRS.Command.PutDeviceData
{
    public class PutDeviceDataCommand : IRequest<DeviceData>
    {
        [JsonIgnore]
        public string? DeviceId { get; set; }

        [JsonPropertyName("name")]

        public string Name { get; set; }

        [JsonPropertyName("brand")]

        public string Brand { get; set; }


        [JsonPropertyName("state")]
        public string State { get; set; }

        public bool IsValid()
        {
            bool valid = !string.IsNullOrEmpty(DeviceId) || !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Brand) || !string.IsNullOrEmpty(State);

            valid = State != Parameters.Available && State != Parameters.InUse && State != Parameters.Inactive ? false : valid;

            return valid;
        }
    }
}
