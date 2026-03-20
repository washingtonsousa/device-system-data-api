using Application.CQRS.Command.PatchDeviceData;
using Application.CQRS.Command.PostDeviceData;
using Domain.Entities;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.GetDeviceData.GetPagedDeviceData
{
    public class PatchDeviceDataHandler : IRequestHandler<PatchDeviceDataCommand, DeviceData>
    {

        private readonly IDeviceDataRepository _deviceDataRepository;
        private readonly IUnityOfWork _unityOfWork;

        public PatchDeviceDataHandler(IDeviceDataRepository deviceDataRepository, IUnityOfWork? unityOfWork)
        {
            _deviceDataRepository = deviceDataRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task<DeviceData> Handle(PatchDeviceDataCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new ArgumentException("Invalid device state");

            var device = await _deviceDataRepository.GetByIdAsync(request.DeviceId, false);

            if (device == null)
                throw new KeyNotFoundException("Device data not found.");

            device.ChangeState(request.State);

            await _unityOfWork.CommitAsync();

            return device;
        }
    }
}
