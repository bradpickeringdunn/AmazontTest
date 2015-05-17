using AmazonTest.Service.Models.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AmazonTest.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IHostSummaryService" in both code and config file together.
    [ServiceContract]
    public interface IHostSummaryService
    {
        [OperationContract]
        HostSummaryResult SummarizeHostStatistics(HostSummaryRequest request);
    }
}
