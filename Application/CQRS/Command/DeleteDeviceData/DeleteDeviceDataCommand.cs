using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.CQRS.Command.DeleteDeviceData
{
    public class DeleteDeviceDataCommand : IRequest
    {
        [FromRoute(Name = "id")]
        public string DeviceId { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(DeviceId);
        }
    }
}
