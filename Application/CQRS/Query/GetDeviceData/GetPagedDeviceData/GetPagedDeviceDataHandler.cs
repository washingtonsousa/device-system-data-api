using Domain.Entities;
using Domain.Entities.Base;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.GetDeviceData.GetPagedDeviceData
{
    public class GetPagedDeviceDataHandler : IRequestHandler<GetPagedDeviceDataQuery, PagedResult<DeviceData>>
    {
        public Task<PagedResult<DeviceData>> Handle(GetPagedDeviceDataQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
