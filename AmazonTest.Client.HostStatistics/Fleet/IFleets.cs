using AmazonTest.Service.Models.Host;
using Backbone.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Client.HostStatistics.Fleet
{
    public interface IFleets
    {
        FleetResult LoadFleetStatus(FleetRequest request);

        HostSummaryResult SummarizeHosts(HostSummaryRequest request);
    }
}
