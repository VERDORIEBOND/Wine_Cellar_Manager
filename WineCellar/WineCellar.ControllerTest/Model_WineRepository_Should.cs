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

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class Model_WineRepository_Should
    {
        private IConfiguration? Configuration { get; set; }
        private WineRecord Expected { get; set; } = new(0, "TestWine", 12.10m, 13.20m, 1, "Malbec", 1, "Nederland", null, 2001, 700, 12.50m, 3, "Beschrijving");
        private int InsertedId { get; set; }

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        [Test, Order(1)]
        public async Task WineRepository_CreateShould()
        {
            // Saving/creating the row in the database, also store Id locally for other tests
            InsertedId = await DataAccess.WineRepo.Create(Expected);
            Assert.IsTrue(InsertedId > 0, "Expected Create to return an integer larger than 0.");

            // Expected started without an id, so adding it here
            Expected = Expected with { Id = InsertedId };

            // Checking if the result is the same as what was expected, records allow for easy comparisons
            WineRecord result = await DataAccess.WineRepo.Get(InsertedId);
            Assert.IsNotNull(result, "WineRecord retrieved from the database is null.");
            Assert.AreEqual(Expected, result);
        }

        [Test, Order(2)]
        public async Task WineRepository_UpdateShould()
        {
            Assert.IsTrue(InsertedId > 0, "Expected InsertedId to return an integer larger than 0.");

            // Change a few variables
            Expected = Expected with { Name = "AlteredName!", Buy = 12.01m, Rating = 5 };

            // Send the update to the database and check if rows affected is 1
            int rowsAffected = await DataAccess.WineRepo.Update(Expected);
            Assert.AreEqual(1, rowsAffected, $"Expected affected rows to be 1, it was {rowsAffected}");

            // Verify the results in database
            WineRecord result = await DataAccess.WineRepo.Get(InsertedId);
            Assert.AreEqual(Expected, result, "Retrieved record does not match updated record");
        }

        [Test, Order(3)]
        public async Task WineRepository_DeleteShould()
        {
            Assert.IsTrue(InsertedId > 0, "Expected InsertedId to return an integer larger than 0.");

            // Send the deletion to the database and check if rows affected is 1
            int rowsAffected = await DataAccess.WineRepo.Delete(InsertedId);
            Assert.AreEqual(1, rowsAffected, $"Expected affected rows to be 1, it was {rowsAffected}");

            // Verify that the row cannot be retrieved
            Assert.ThrowsAsync<InvalidOperationException>(() => DataAccess.WineRepo.Get(InsertedId));
        }
    }
}
