using Application.CQRS.Command.PatchDeviceData;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;

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
                throw new DeviceValidationException("Invalid patch data. At least one field must be provided and state, if present, must be a valid value.");

            var device = await _deviceDataRepository.GetByIdAsync(request.DeviceId, false);

            if (device == null)
                throw new DeviceNotFoundException(request.DeviceId!);

            device.Patch(request.Name, request.Brand, request.State);

            await _unityOfWork.CommitAsync();

            return device;
        }
    }
}
