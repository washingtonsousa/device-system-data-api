using Application.CQRS.Command.PatchDeviceData;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.PutDeviceData.GetPagedDeviceData
{

    public class PutDeviceDataHandler : IRequestHandler<PutDeviceDataCommand, DeviceData>
    {
        private readonly IDeviceDataRepository _deviceDataRepository;
        private readonly IUnityOfWork _unityOfWork;

        public PutDeviceDataHandler(IDeviceDataRepository deviceDataRepository, IUnityOfWork? unityOfWork)
        {
            _deviceDataRepository = deviceDataRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<DeviceData> Handle(PutDeviceDataCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("Invalid device data.");

            var device = await _deviceDataRepository.GetByIdAsync(request.DeviceId, false);

            if (device == null)
                throw new KeyNotFoundException("Device data not found.");

            device.Update(request.Name, request.Brand, request.State);

            await _unityOfWork.CommitAsync();

            return device;
        }
    }
}
