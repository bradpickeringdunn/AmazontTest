using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AmazonTest.Domain.Services;
using FakeItEasy;
using AmazonTest.Domain.Persistance;
using Backbone.Logging;
using AmazonTest.Service.Models.Host;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using AmazonTest.Domain.Validation;
using AmazonTest.Domain.Services.Fleet;

namespace AmazonTest.Domain.Tests.Services.Fleet
{
    [TestClass]
    public class FleetServiceTestFixture
    {
        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ensure_FleetService_LoadFleet_Guards_The_Result()
        {
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();

            var fleetService = new FleetService(repo, logger);

            var request = new FleetRequest("File location");
            var result = fleetService.LoadFleet(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Ensure_FleetService_LoadFleet_Guards_The_Result_FileName()
        {
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();

            var fleetService = new FleetService(repo, logger);

            var request = new FleetRequest(null);
            var result = fleetService.LoadFleet(request);
        }

        [TestMethod]
        public void Ensure_FleetService_LoadFleet_LogsError_If_Loaded_File_IsNullOrEmpty()
        {
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();

            var fleetService = new FleetService(repo, logger);

            A.CallTo(() => repo.LoadFile(null)).WithAnyArguments().Returns(null);

            var request = new FleetRequest("Test path");
            var result = fleetService.LoadFleet(request);

            Assert.IsTrue(result.Notifications.HasErrors);
            A.CallTo(() => logger.Error(null)).WithAnyArguments().MustHaveHappened();
        }

        [TestMethod]
        public void Ensure_FleetService_LoadFleet_Formats_Re()
        {
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();

            var liines = new List<string>()
            {
                "{0}{1}".FormatLiteralArguments("1, M1, 5, 0,0,0,0,0",Constants.CaridgeReturn),
                "{0}{1}".FormatLiteralArguments("2, M2, 3, 1,0,0",Constants.CaridgeReturn),
                "{0}{1}".FormatLiteralArguments("3, M3, 5, 1,1,1,1,1",Constants.CaridgeReturn)
            };

            var loadedFile = new StringBuilder();
            liines.ForEach((line) => { loadedFile.Append(line); });
            
            var fleetService = new FleetService(repo, logger);

            A.CallTo(()=>repo.LoadFile(null)).WithAnyArguments().Returns(loadedFile.ToString());

            var request = new FleetRequest("Test path");
            var result = fleetService.LoadFleet(request);

            Assert.AreEqual(result.Hosts.Count(), liines.Count);
            Assert.IsFalse(result.Notifications.HasErrors);
            
        }

        [TestMethod]
        public void Ensure_FleetService_LoadFleet_Formats_ReadFile_Into_List_Of_Lines_If_Seperator_Is_CaridgeReturn()
        {
            var repo = A.Fake<IFileRepository>();
            var logger = A.Fake<ILogger>();

            string host = "1,M5,0,1,1,0,1" ;
            var expectedResult = new HostDto(){
            Id= 1,
            Instance="M1",
            N = 5, 
            FilledSlots = 3
            };

            var hostValidation = new HostValidation(logger);

            var result = hostValidation.ValidateAndCreateHost(host);

            Assert.AreEqual(expectedResult.FilledSlots, result.FilledSlots);
        }
    }
}
