using AmazonTest.Service.Models.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Domain.Services.Fleet
{
    public interface IFleetService
    {
        FleetResult LoadFleet(FleetRequest request);
    }
}
