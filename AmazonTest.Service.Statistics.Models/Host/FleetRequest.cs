using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Service.Models.Host
{
    [DataContract]
    public class FleetRequest
    {
        public FleetRequest(string fileLocation)
        {
            FileLocation = fileLocation;
        }

        [DataMember]
        public string FileLocation { get; set; }
    }
}
