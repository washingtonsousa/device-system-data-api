using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Command.PostDeviceData
{
    public class PostDeviceDataHandler : IRequestHandler<PostDeviceDataCommand>
    {
        public async Task Handle(PostDeviceDataCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
