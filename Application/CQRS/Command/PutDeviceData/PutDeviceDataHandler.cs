using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;

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
                throw new DeviceValidationException("Invalid device data.");

            var device = await _deviceDataRepository.GetByIdAsync(request.DeviceId, false);

            if (device == null)
                throw new DeviceNotFoundException(request.DeviceId!);

            device.Update(request.Name, request.Brand, request.State);

            await _unityOfWork.CommitAsync();

            return device;
        }
    }
}
