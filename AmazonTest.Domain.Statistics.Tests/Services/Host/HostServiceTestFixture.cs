using AmazonTest.Domain.Persistance;
using AmazonTest.Domain.Services;
using AmazonTest.Service.Models.Host;
using Backbone.Logging;
using FakeItEasy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace AmazonTest.Domain.Statistics.Tests.Hosts
{
    [TestClass]
    public class HostStatisticsTestFixture
    {
        [TestMethod]
        public void Ensure_HostService_Can_Summarize_Only_Empty_Hosts()
        {
            int hostId = 1;
            int hostSlots = 2;
            int filledSlots = 0;
            
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots+ 4 , FilledSlots=filledSlots +3},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots+ 4 , FilledSlots=filledSlots +3}
            };
                                    
            var result = hostService.GetEmptyHosts(hosts);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Instance == Constants.Instance.M1);
            Assert.IsTrue(result.First().FilledSlots == 0);
        }

        [TestMethod]
        public void Ensure_HostService_Can_Summarize_Empty_Hosts_Ordered_By_Descending_Instance()
        {
            int hostId = 1;
            int hostSlots = 2;
            int filledSlots = 0;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},

            };

            var result = hostService.GetEmptyHosts(hosts);

            Assert.IsTrue(result.Count == hosts.Count);

            Assert.AreEqual(result[0].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[1].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[2].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[3].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[4].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[5].Instance, Constants.Instance.M3);

        }

        [TestMethod]
        public void Ensure_HostService_Can_Summarize_Only_Full_Hosts()
        {
            int hostId = 1;
            int hostSlots = 2;
            int filledSlots = 2;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots}
            };

            var result = hostService.GetFullHosts(hosts);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Instance == Constants.Instance.M1);
            Assert.IsTrue(result.First().FilledSlots == filledSlots);
        }

        [TestMethod]
        public void Ensure_HostService_Can_Summarize_Full_Hosts_Ordered_By_Descending_Instance()
        {
            int hostId = 1;
            int hostSlots = 2;
            int filledSlots = 2;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},

            };

            var result = hostService.GetFullHosts(hosts);

            Assert.IsTrue(result.Count == hosts.Count);

            Assert.AreEqual(result[0].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[1].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[2].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[3].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[4].Instance, Constants.Instance.M3);
            Assert.AreEqual(result[5].Instance, Constants.Instance.M3);

        }

        [TestMethod]
        public void Ensure_HostService_GetTheMostFullHosts_Can_Summarize_Only_Mostly_Full_Hosts()
        {
            int hostId = 1;
            int hostSlots = 4;
            int filledSlots = 2;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots+ 4 , FilledSlots=filledSlots +3},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots+ 4 , FilledSlots=filledSlots +3}
            };

            var result = hostService.GetTheMostFullHost(hosts);

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.First().Instance == Constants.Instance.M1);
            Assert.IsTrue(result.First().FilledSlots == filledSlots);
        }

        [TestMethod]
        public void Ensure_HostService_GetTheMostFullHosts_Can_Summarize_Hosts_Ordered_By_Descending_Instance()
        {
            int hostId = 1;
            int hostSlots = 6;
            int filledSlots = 2;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},

            };

            var result = hostService.GetFullHosts(hosts);

            Assert.IsTrue(result.Count == hosts.Count);

            Assert.AreEqual(result[0].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[1].Instance, Constants.Instance.M1);
            Assert.AreEqual(result[2].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[3].Instance, Constants.Instance.M2);
            Assert.AreEqual(result[4].Instance, Constants.Instance.M3);
            Assert.AreEqual(result[5].Instance, Constants.Instance.M3);

        }

        [TestMethod]
        public void Ensure_HostService_CreateSummaryEntry_Returns_()
        {
            int hostId = 1;
            int hostSlots = 6;
            int filledSlots = 2;

            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();
            var hostService = new HostService(repo, logger);
            var hosts = new List<HostDto>()
            {
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M3,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M2,N = hostSlots, FilledSlots=filledSlots},
                new HostDto(){Id = hostId,Instance = Constants.Instance.M1,N = hostSlots, FilledSlots=filledSlots},

            };
            
            var result = hostService.CreateSummaryEntry(hosts,Constants.SummaryTitles.Empty);

            Assert.Fail();

        }

    }
}
