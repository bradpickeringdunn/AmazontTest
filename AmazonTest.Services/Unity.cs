using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Services;
using AmazonTest.Domain.Services.Fleet;
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
            containers.RegisterType<IFleetService, FleetService>();
            containers.RegisterType<ILogger, DebugLogger>();
        }
    }
}