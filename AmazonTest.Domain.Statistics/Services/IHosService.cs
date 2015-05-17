using AmazonTest.Service.Models.Host;
using Backbone.Services.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Domain.Services
{
    public interface IHosService
    {
        HostSummaryResult SummarizeHostStatistics(HostSummaryRequest request);
    }
}
