using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonTest.Client.HostStatistics
{
    public class Config
    {
        public static string HostStatisticsFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["HostStatisticsFilePath"].ToString();
            }
        }

        public static string FleetStateFilePath
        {
            get
            {
                return ConfigurationManager.AppSettings["FleetStateFilePath"].ToString();
            }
        }

        public static int NumberOfPropertiesInHost
        {
            get
            {
                var hostPropertyCount = ConfigurationManager.AppSettings["hostPropertyCount"].ToString();

                int propertyCount = 0;

                if (!int.TryParse(hostPropertyCount, out propertyCount))
                {
                    //Log error
                }

                return propertyCount;
            }
        }
    }
}
