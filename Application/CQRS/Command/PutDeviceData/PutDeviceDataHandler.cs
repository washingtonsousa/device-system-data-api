using Application.CQRS.Command.PatchDeviceData;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.PutDeviceData.GetPagedDeviceData
{
    public class PutDeviceDataHandler : IRequestHandler<PutDeviceDataCommand, DeviceData>
    {
        public Task<DeviceData> Handle(PutDeviceDataCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
