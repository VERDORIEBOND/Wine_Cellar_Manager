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

        private StorageLocation _ToInsertRecord { get; set; } = new StorageLocation(1, "ABCdef", 9001, 9001);

        private StorageLocation[] _ToInsertRecords { get; set; } = new StorageLocation[]
        {
            new(1, "TestA", 2, 1),
            new(1, "TestA", 2, 2),
            new(1, "TestA", 2, 3),
            new(1, "TestA", 2, 4),
            new(1, "TestA", 2, 5)
        };
            

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        private bool CompareLocations(StorageLocation loc1, StorageLocation loc2)
        {
            return loc1.IdWine == loc2.IdWine
                && loc1.Shelf.Equals(loc2.Shelf)
                && loc1.Row == loc2.Row
                && loc1.Col == loc2.Col;
        }

        [Test]
        public async Task LocationRepository_GetAllShould()
        {
            // Saving/creating the row in the database, also store Id locally for other tests
            var result = (await DataAccess.LocationRepo.GetAll()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test, Order(2)]
        public async Task LocationRepository_GetByWineShould()
        {
            // Records that are expected to be found in the database, these are created in a postdeployment script for testing purposes
            List<StorageLocation> result = (await DataAccess.LocationRepo.GetByWine(1)).ToList();

            int amtFound = 0;

            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                foreach (StorageLocation record in result)
                {
                    if (CompareLocations(record, _ToInsertRecords[i]))
                    {
                        amtFound++;
                        break;
                    }
                }
            }

            Assert.AreEqual(_ToInsertRecords.Length, amtFound);
        }

        [Test, Order(1)]
        public async Task LocationRepository_InsertShould()
        {
            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                // Add the record to the database
                await DataAccess.LocationRepo.Create(_ToInsertRecords[i]);
            }

            // Retrieve all locations for used WineId
            List<StorageLocation> result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();

            int amtFound = 0;

            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                foreach (StorageLocation record in result)
                {
                    if (CompareLocations(record, _ToInsertRecords[i]))
                    {
                        amtFound++;
                        break;
                    }
                }
            }

            Assert.AreEqual(_ToInsertRecords.Length, amtFound);
        }

        [Test, Order(3)]
        public async Task LocationRepository_DeleteShould()
        {
            // Retrieve all locations for used WineId, to verify the entry actually exists already
            List<StorageLocation> result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();
            int amtFound = 0;

            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                foreach (StorageLocation record in result)
                {
                    if (CompareLocations(record, _ToInsertRecords[i]))
                    {
                        amtFound++;
                        break;
                    }
                }
            }

            Assert.AreEqual(_ToInsertRecords.Length, amtFound);

            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                // Delete the location from the database
                await DataAccess.LocationRepo.Delete(_ToInsertRecords[i]);
            }

            // Retrieve all locations for used WineId again to verify deletion
            result = (await DataAccess.LocationRepo.GetByWine(_ToInsertRecord.IdWine)).ToList();
            for (int i = 0; i < _ToInsertRecords.Length; i++)
            {
                foreach (StorageLocation record in result)
                {
                    if (CompareLocations(record, _ToInsertRecords[i]))
                    {
                        Assert.Fail($"Record still exists with IdWine: {record.IdWine} | Shelf {record.Shelf} | Row {record.Row} | Col {record.Col}");
                        break;
                    }
                }
            }
        }
    }
}
