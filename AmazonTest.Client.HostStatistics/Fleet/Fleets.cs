using AmazonTest.Domain.Services.Fleet;
using AmazonTest.Domain.Services.Host;
using AmazonTest.Service.Models.Host;
using Backbone.Logging;
using System;

namespace AmazonTest.Client.HostStatistics.Fleet
{
    public class Fleets : IFleets
    {
        private ILogger Logger { get; set; }
        const string GeneralError = "A general exception occured.";

        public Fleets(ILogger logger)
        {
            Logger = logger;
        }

        private IFleetService FleetService{get;set;}

        private IHosService HostService { get; set; }

        public Fleets(IFleetService fleetStatus, IHosService hostService)
        {
            FleetService = fleetStatus;
            HostService = hostService;
        }
        
        public FleetResult LoadFleetStatus(FleetRequest request)
        {
            var result = new FleetResult();

            try
            {
                result = FleetService.LoadFleet(request);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Notifications.Add(GeneralError);
            }

            return result;
        }

        public HostSummaryResult SummarizeHosts(HostSummaryRequest request)
        {
            var result = new HostSummaryResult();

            try
            {
                result = HostService.SummarizeHostStatistics(request);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Notifications.Add(GeneralError);
            }

            return result;
        }

    }
}
