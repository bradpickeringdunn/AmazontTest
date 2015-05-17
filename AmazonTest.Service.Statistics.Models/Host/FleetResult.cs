using Backbone.Services.Results;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace AmazonTest.Service.Models.Host
{
    [DataContract]
    public class FleetResult : GenericServiceResult
    {
        [DataMember]
        public IEnumerable<HostDto> Hosts { get; set; }
    }
}
