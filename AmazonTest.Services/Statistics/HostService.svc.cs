using AmazonTest.Service.Models.Host;
using Backbone.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AmazonTest.Services.Statistics
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "HostService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select HostService.svc or HostService.svc.cs at the Solution Explorer and start debugging.
    public class HostService : IHostService
    {
        public HostService()
        {
            //Unity.Initalize();
        }

        public FleetResult LoadFleetState()
        {
            throw new NotImplementedException();
        }
                
    }
}
