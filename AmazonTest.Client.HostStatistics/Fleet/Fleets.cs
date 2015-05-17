using AmazonTest.Domain.Services;
using AmazonTest.Service.Models.Host;
using Backbone.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Client.HostStatistics.Fleet
{
    public class Fleets : IFleets
    {
        private IFleetService FleetService{get;set;}

        private IHosService HostService { get; set; }

        public Fleets(IFleetService fleetStatus, IHosService hostService)
        {
            FleetService = fleetStatus;
            HostService = hostService;
        }
        
        public FleetResult LoadFleetStatus(FleetRequest request)
        {
            return FleetService.LoadFleet(request);
        }

        public HostSummaryResult SummarizeHosts(HostSummaryRequest request)
        {
            return HostService.SummarizeHostStatistics(request);
        }

    }
}
