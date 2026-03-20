using Application.CQRS.Query.GetDeviceData.GetPagedDeviceData;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.CQRS.Query.GetDeviceData.GetSingleDeviceData
{
    public class GetSingleDeviceDataHandler : IRequestHandler<GetDeviceDataQuery, DeviceData>
    {
        private readonly IDeviceDataRepository _deviceDataRepository;

        public GetSingleDeviceDataHandler(IDeviceDataRepository deviceDataRepository)
        {
            _deviceDataRepository = deviceDataRepository;
        }
        public async  Task<DeviceData> Handle(GetDeviceDataQuery request, CancellationToken cancellationToken)
        {
            return await _deviceDataRepository.GetByIdAsync(request.DeviceId);
        }
    }
}
