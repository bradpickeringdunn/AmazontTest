using AmazonTest.Client.HostStatistics.FleetService;
using AmazonTest.Client.HostStatistics.HostSummaryService;
using Backbone.Logging;
using Microsoft.Practices.Unity;

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

            containers.RegisterType<FleetService.IFleetService, FleetService.FleetServiceClient>();
            containers.RegisterType<IHostSummaryService, HostSummaryServiceClient>();
         
        }
    }
}
