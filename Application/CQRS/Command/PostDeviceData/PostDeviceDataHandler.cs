using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.UnityOfWork;
using MediatR;

namespace Application.CQRS.Command.PostDeviceData
{
    public class PostDeviceDataHandler : IRequestHandler<PostDeviceDataCommand>
    {
        private readonly IDeviceDataRepository _deviceDataRepository;
        private readonly IUnityOfWork _unityOfWork;

        public PostDeviceDataHandler(IDeviceDataRepository deviceDataRepository, IUnityOfWork? unityOfWork)
        {
            _deviceDataRepository = deviceDataRepository;
            _unityOfWork = unityOfWork;
        }

        public async Task Handle(PostDeviceDataCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid())
                throw new DeviceValidationException("Invalid device data.");

            var device = await _deviceDataRepository.AddAsync(DeviceData.CreateDeviceData(request.Name,request.Brand,request.State));

            
            await _unityOfWork.CommitAsync();
        }
    }
}
