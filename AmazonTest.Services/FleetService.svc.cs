using AmazonTest.Service.Models.Host;
using Backbone.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Microsoft.Practices.Unity;

namespace AmazonTest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "FleetService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select FleetService.svc or FleetService.svc.cs at the Solution Explorer and start debugging.
    public class FleetService : IFleetService
    {
        ILogger Logger; 
        Domain.Services.Fleet.IFleetService fleetService;

        public FleetService()
        {
            Unity.Initalize();
            Logger = Unity.Containers.Resolve<ILogger>();
            fleetService = Unity.Containers.Resolve<Domain.Services.Fleet.IFleetService>();
        }

        public FleetResult LoadFleetState(FleetRequest request)
        {
            var result = new FleetResult();

            try
            {
                result = fleetService.LoadFleet(request);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
                result.Notifications.Add("A general service exception occured");
            }

            return result;
        }
    }
}
