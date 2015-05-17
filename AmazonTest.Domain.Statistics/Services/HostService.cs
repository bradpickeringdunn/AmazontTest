using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Properties;
using AmazonTest.Service.Models.Host;
using Backbone.ErrorHandling;
using Backbone.Logging;
using Backbone.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Domain.Services
{
    public class HostService : IHosService
    {
        private NotificationCollection Notifications = new NotificationCollection();
        private ILogger Logger;
        private IFileRepository Repository { get; set; }

        /// <summary>
        /// Constructor injecting dependancies
        /// </summary>
        public HostService(IFileRepository repository, ILogger logger)
        {
            this.Logger = logger;
            this.Repository = repository;
        }

        public HostSummaryResult SummarizeHostStatistics(HostSummaryRequest request)
        {
            var hostSummaryResults = new HostSummaryResult();
            var results = new Dictionary<string,List<HostDto>>();
            results.Add(Constants.SummaryTitles.Empty, GetEmptyHosts(request.Hosts));
            results.Add(Constants.SummaryTitles.FULL, GetFullHosts(request.Hosts));
            results.Add(Constants.SummaryTitles.MOST_FILLED, GetTheMostFullHost(request.Hosts));
        
            SaveSummariesToDisk(results);

            hostSummaryResults.SummarizedHosts = results;

            return hostSummaryResults;
        }

        private void SaveSummariesToDisk(IDictionary<string,List<HostDto>> summaries)
        {
            var fileLines = new List<string>();

            foreach (var summary in summaries)
            {
                fileLines.Add(CreateSummaryEntry(summary.Value, summary.Key));
            }

            System.IO.File.WriteAllLines(Config.HostStatisticsFilePath, fileLines);
        }

        public string CreateSummaryEntry(List<HostDto> hosts, string headder)
        {
            var sb = new StringBuilder();

            sb.Append("{0}:".FormatLiteralArguments(headder));

            foreach (var host in hosts)
            {
                sb.Append("{0}={1};".FormatLiteralArguments(host.Instance, host.FilledSlots));
            }

            return sb.ToString();
        }

        public List<HostDto> GetTheMostFullHost(IEnumerable<HostDto> hosts)
        {
            var result = new List<HostDto>();

            foreach (var host in hosts)
            {
                ValidateHost(host);
                if (!Notifications.HasErrors && host.FilledSlots != 0 && host.N != host.FilledSlots)
                {
                    result.Add(host);
                }
            }

            result = result.OrderBy(x => x.Instance).ToList();

            return result;
        }

        public List<HostDto> GetFullHosts(IEnumerable<HostDto> hosts)
        {
            var result = new List<HostDto>();

            foreach (var host in hosts)
            {
                ValidateHost(host);
                if (!Notifications.HasErrors && host.FilledSlots == host.N)
                {
                    result.Add(host);
                }
            }

            result = result.OrderBy(x => x.Instance).ToList();

            return result;
        }

        public List<HostDto> GetEmptyHosts(IEnumerable<HostDto> hosts)
        {
            var result = new List<HostDto>();

            foreach (var host in hosts)
            {
                ValidateHost(host);
                if (host.FilledSlots == 0 && !Notifications.HasErrors)
                {
                    result.Add(host);
                }
            }
            
            result = result.OrderBy(x => x.Instance).ToList();

            return result;
        }

        private void ValidateHost(HostDto host)
        {
            if (host.FilledSlots > host.FilledSlots)
            {
                Logger.Error(ErrorMessages.ErrorsummarizingHosts);
                Notifications.Add(ErrorMessages.ErrorsummarizingHosts);
            }
        }
    }
}
