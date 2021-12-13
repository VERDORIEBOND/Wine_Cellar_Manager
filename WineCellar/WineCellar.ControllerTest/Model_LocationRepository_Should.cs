using Controller;
using Microsoft.Extensions.Configuration;
using WineCellar.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.ControllerTest.Utilities;
using System.Diagnostics;

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class Model_LocationRepository_Should
    {
        private IConfiguration? Configuration { get; set; }

        private StorageLocationRecord _ToInsertRecord { get; set; } = new StorageLocationRecord(1, "ABCdef", 9001, 9001);

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        [Test]
        public async Task LocationRepository_GetAllShould()
        {
            // Saving/creating the row in the database, also store Id locally for other tests
            var result = (await DataAccess.LocationRepo.GetAll()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test, Order(1)]
        public async Task LocationRepository_GetByWineShould()
        {
            // Records that are expected to be found in the database, these are created in a postdeployment script for testing purposes
            StorageLocationRecord[] expectedRecords = new StorageLocationRecord[]
            {
                new(1, "A", 2, 1),
                new(1, "A", 2, 2),
                new(1, "A", 2, 3),
                new(1, "A", 2, 4),
                new(1, "A", 2, 5)
            };

            List<StorageLocationRecord> result = (await DataAccess.LocationRepo.GetByWine(1)).ToList();

            Assert.AreEqual(5, result.Count);

            for (int i = 0; i < expectedRecords.Length; i++)
            {
                Assert.Contains(expectedRecords[i], result, $"Expected record at index {i} to be present");
            }
        }

        [Test, Order(2)]
        public async Task LocationRepository_InsertShould()
        {
            // Add the record to the database
            await DataAccess.LocationRepo.Create(_ToInsertRecord);

            // Retrieve all locations for used WineId
            List<StorageLocationRecord> result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();

            Assert.Contains(_ToInsertRecord, result);
        }

        [Test, Order(3)]
        public async Task LocationRepository_DeleteShould()
        {
            // Retrieve all locations for used WineId, to verify the entry actually exists already
            List<StorageLocationRecord> result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();
            Assert.Contains(_ToInsertRecord, result);

            // Delete the location from the database
            int affectedRows = await DataAccess.LocationRepo.Delete(_ToInsertRecord);
            Assert.AreEqual(1, affectedRows, "Expected affected rows after deletion to be 1");

            // Retrieve all locations for used WineId again to verify deletion
            result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();
            foreach (StorageLocationRecord record in result)
            {
                if (record == _ToInsertRecord)
                    Assert.Fail($"The deleted record is still in the database {record}");
            }
        }
    }
}
