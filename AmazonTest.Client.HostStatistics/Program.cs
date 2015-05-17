using AmazonTest.Client.HostStatistics.FleetService;
using AmazonTest.Client.HostStatistics.HostSummaryService;
using AmazonTest.Service.Models.Host;
using Microsoft.Practices.Unity;
using System;
using System.Text;

namespace AmazonTest.Client.HostStatistics
{
    class Program
    {
        static void Main()
        {
            var fleetService = new FleetServiceClient();
            var hostSummaryService = new HostSummaryServiceClient();

            Console.WriteLine("Loading the fellt state......");

            var fleetRequest = new FleetRequest(Config.FleetStateFilePath);
            var fleetStatusResult = fleetService.LoadFleetState(fleetRequest);

            if (!fleetStatusResult.Notifications.HasErrors)
            {
                Console.WriteLine("\r\r");
                Console.WriteLine("Fleet state results.");

                DisplayFleetStatus(fleetStatusResult);

                Console.WriteLine("The follow is a smmary of the fleet status");
                Console.WriteLine("\r\r");

                var summaryRequest = new HostSummaryRequest(Config.HostStatisticsFilePath)
                {
                    Hosts = fleetStatusResult.Hosts
                };

                var result = hostSummaryService.SummarizeHostStatistics(summaryRequest);

                DisplaySummary(result);
            }
            else
            {
                Console.Write("The following errors occured");
                foreach (var error in fleetStatusResult.Notifications)
                {
                    Console.WriteLine(error);
                }
            }

            Console.ReadLine();
        }

        private static void DisplaySummary(HostSummaryResult result)
        {
            foreach (var host in result.SummarizedHosts)
            {
                var sb = new StringBuilder();

                sb.Append("{0}:".FormatLiteralArguments(host.Key));

                foreach (var instance in host.Value)
                {
                    sb.Append("{0}={1};".FormatLiteralArguments(instance.Instance, instance.FilledSlots));
                }

                Console.WriteLine(sb);
            }
        }

        private static void DisplayFleetStatus(FleetResult fleetStatusResult)
        {
            foreach (var host in fleetStatusResult.Hosts)
            {
                Console.WriteLine(
                    "Id = {0}, Instance = {1}, N = {2}, Filled slots = {3}"
                    .FormatLiteralArguments(
                    host.Id,
                    host.Instance,
                    host.N,
                    host.FilledSlots)
                    );
            }
        }
    }
}
