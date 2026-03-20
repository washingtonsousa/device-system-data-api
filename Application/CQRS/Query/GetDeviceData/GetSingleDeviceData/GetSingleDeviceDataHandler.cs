using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Query.GetDeviceData.GetSingleDeviceData
{
    public class GetSingleDeviceDataHandler : IRequestHandler<GetDeviceDataQuery, DeviceData>
    {
        public Task<DeviceData> Handle(GetDeviceDataQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
