using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.DeleteDeviceData
{
    public class DeleteDeviceDataHandler : IRequestHandler<DeleteDeviceDataCommand>
    {
        private readonly IDeviceDataRepository _deviceDataRepository;
        private readonly IUnityOfWork _unityOfWork;

        public DeleteDeviceDataHandler(IDeviceDataRepository deviceDataRepository, IUnityOfWork? unityOfWork)
        {
            _deviceDataRepository = deviceDataRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Handle(DeleteDeviceDataCommand request, CancellationToken cancellationToken)
        {
           var device = await _deviceDataRepository.GetByIdAsync(request.DeviceId, false);

            if(device == null)
                throw new KeyNotFoundException("Device data not found.");

            if (device.IsInUse)
                throw new InvalidOperationException("Cannot delete device data that is currently in use.");


            _deviceDataRepository.RemoveAsync(device);

            await _unityOfWork.CommitAsync();

        }
    }
}
