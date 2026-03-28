using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;

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

            if (device == null)
                throw new DeviceNotFoundException(request.DeviceId!);

            if (device.IsInUse)
                throw new DeviceStateConflictException("Cannot delete a device that is currently in use.");


            _deviceDataRepository.RemoveAsync(device);

            await _unityOfWork.CommitAsync();

        }
    }
}
