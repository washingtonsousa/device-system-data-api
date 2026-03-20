using MediatR;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.CQRS.Query.GetDeviceData.GetPagedDeviceData
{
    public class GetDeviceDataQuery : IRequest<DeviceData>
    {
        [FromRoute(Name = "id")]

        public string? DeviceId { get; set; }
    }
}