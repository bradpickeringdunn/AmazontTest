using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Properties;
using AmazonTest.Domain.Validation;
using AmazonTest.Service.Models.Host;
using Backbone.ErrorHandling;
using Backbone.Logging;
using Backbone.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AmazonTest.Domain.Services
{
    public class FleetService : IFleetService
    {
        private List<string> Instances = new List<string>();
        private NotificationCollection notifications = new NotificationCollection();

        private ILogger Logger { get; set; }
        private IFileRepository Repository { get; set; }

        public FleetService(IFileRepository repository, ILogger logger)
        {
            Guardian.ArgumentNotNull(logger, "logger");
            Guardian.ArgumentNotNull(logger, "logger");

            this.Repository = repository;
            this.Logger = logger;

            Instances.Add(Constants.Instance.M1);
            Instances.Add(Constants.Instance.M2);
            Instances.Add(Constants.Instance.M3);
        }

        public FleetResult LoadFleet(FleetRequest request)
        {
            Guardian.ArgumentNotNull(request, "request");
            Guardian.ArgumentNotNull(request.FileLocation, "request.FileLocation");

            var result = new FleetResult();
            string[] hostEnteries = new string[0];

            var fleetStateFile = Repository.LoadFile(request.FileLocation);

            if (fleetStateFile.IsNullOrEmpty())
            {
                Logger.Error(ErrorMessages.FileNotFound);
                result.Notifications.Add(ErrorMessages.FileNotFound);
            }
            else
            {
                if (fleetStateFile.Contains("{0}{1}".FormatLiteralArguments(Constants.CaridgeReturn, Constants.NewLine)))
                {
                    hostEnteries = fleetStateFile.Split("{0}{1}".FormatLiteralArguments(Constants.CaridgeReturn, Constants.NewLine).ToCharArray());
                }
                else if (fleetStateFile.Contains(Constants.CaridgeReturn))
                {
                    hostEnteries = fleetStateFile.Split(Constants.CaridgeReturn.ToCharArray());
                }
                else
                {
                    result.Notifications.Add(ErrorMessages.IncorrectFfileState);
                }

                if (!result.Notifications.HasErrors)
                {
                    hostEnteries = hostEnteries.Where(x => x.IsNotNullOrEmpty()).ToArray();
                    
                    if (hostEnteries.Length == 0)
                    {
                        result.Notifications.Add(ErrorMessages.IncorrectFfileState);
                    }
                    else
                    {
                        var hostValidation = new HostValidation(Logger);
                        var createdHosts = new List<HostDto>();

                        foreach (var host in hostEnteries)
                        {
                            var createdHost = hostValidation.CreateHost(host);

                            if (createdHost.IsNotNull())
                            {
                                createdHosts.Add(createdHost);
                            }
                        }

                        result.Hosts = createdHosts;
                    }
                }
            }
            return result;
        }        
    }
}
