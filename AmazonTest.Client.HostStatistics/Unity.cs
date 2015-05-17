using AmazonTest.Client.HostStatistics.Fleet;
using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Services;
using AmazonTest.Domain.Services.Fleet;
using AmazonTest.Domain.Services.Host;
using Backbone.Logging;
using Backbone.Repository;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using domain = AmazonTest.Domain.Services;

namespace AmazonTest.Client.HostStatistics
{
    internal static class Unity 
    {
        public static IUnityContainer containers;

        public static void Initalize()
        {
            containers = new UnityContainer();

            // This will register all types with a ISample/Sample naming convention 
            containers.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            containers.RegisterType<IFileRepository, FileRepository>();
            containers.RegisterType<IFleetService, domain.Fleet.FleetService>();
            containers.RegisterType<ILogger, DebugLogger>();
            containers.RegisterType<IHosService, HostService>();
            containers.RegisterType<IFleets, Fleets>();
         
        }
    }
}
