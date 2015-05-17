using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Services;
using AmazonTest.Domain.Services.Fleet;
using AmazonTest.Domain.Services.Host;
using Backbone.Logging;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AmazonTest.Services
{
    public static class Unity
    {
        public static IUnityContainer Containers;

        public static void Initalize()
        {
            Containers = new UnityContainer();

            // This will register all types with a ISample/Sample naming convention 
            Containers.RegisterTypes(
                AllClasses.FromLoadedAssemblies(),
                WithMappings.FromMatchingInterface,
                WithName.Default);

            Containers.RegisterType<IFileRepository, FileRepository>();
            Containers.RegisterType<Domain.Services.Fleet.IFleetService, Domain.Services.Fleet.FleetService>();
            Containers.RegisterType<ILogger, DebugLogger>();
            Containers.RegisterType<IHosService, HostService>();
            Containers.RegisterType<IHosService, HostService>();

        }
    }
}