using Domain.Constants;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using System.Text.Json.Serialization;

namespace Application.CQRS.Command.PostDeviceData
{
    public class PostDeviceDataCommand : IRequest
    {
        [JsonPropertyName("name")]

        public string Name { get;  set; }

        [JsonPropertyName("brand")]

        public string Brand { get;  set; }


        [JsonPropertyName("state")]
        public string State { get; set; }

        public bool IsValid()
        {
            bool valid =  !string.IsNullOrEmpty(Name) || !string.IsNullOrEmpty(Brand) || !string.IsNullOrEmpty(State);

            valid = State != Parameters.Available && State != Parameters.InUse && State != Parameters.Inactive ? false : valid;

            return valid;
        }
    }
}
