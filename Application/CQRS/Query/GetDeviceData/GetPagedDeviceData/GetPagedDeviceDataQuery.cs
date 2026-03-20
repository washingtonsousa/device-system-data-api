using MediatR;
using Domain.Entities.Base;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.CQRS.Query.GetDeviceData.GetPagedDeviceData
{
    public class GetPagedDeviceDataQuery : IRequest<PagedResult<DeviceData>>
    {

        [FromQuery(Name = "page")]
        public int PageNumber { get; set; } = 1;

        [FromQuery(Name = "deviceId")]

        public string? DeviceId { get; set; }

        [FromQuery(Name = "brand")]

        public string? Brand { get; set; }

        [FromQuery(Name = "state")]

        public string? State { get; set; }

        [FromQuery(Name = "page_size")]
        public int PageSize { get; set; } = 4;
    }
}