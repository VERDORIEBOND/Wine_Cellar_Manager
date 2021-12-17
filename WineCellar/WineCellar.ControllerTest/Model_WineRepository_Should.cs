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
    public class Model_WineRepository_Should
    {
        private IConfiguration? Configuration { get; set; }
        private Wine Expected { get; set; } = new(0, "TestWine", 12.10m, 13.20m, 1, "Malbec", 1, "Nederland", null, 2001, 700, 12.50m, 3, "Beschrijving");
        private int InsertedId { get; set; }

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        private static bool CompareWines(Wine wine1, Wine wine2)
        {
            return wine1.Id == wine2.Id
                && wine1.Name.Equals(wine2.Name)
                && wine1.Buy.Equals(wine2.Buy)
                && wine1.Sell.Equals(wine2.Sell)
                && wine1.TypeId == wine2.TypeId
                && wine1.CountryId == wine2.CountryId
                && wine1.Year == wine2.Year
                && wine1.Content == wine2.Content
                && wine1.Alcohol.Equals(wine2.Alcohol)
                && wine1.Rating == wine2.Rating
                && wine1.Description.Equals(wine2.Description);
        }

        [Test, Order(1)]
        public async Task WineRepository_CreateShould()
        {
            // Saving/creating the row in the database, also store Id locally for other tests
            InsertedId = await DataAccess.WineRepo.Create(Expected);
            Assert.IsTrue(InsertedId > 0, "Expected Create to return an integer larger than 0.");

            // Expected started without an id, so adding it here
            Expected.Id = InsertedId;

            // Checking if the result is the same as what was expected, records allow for easy comparisons
            Wine result = await DataAccess.WineRepo.Get(InsertedId);
            Assert.IsNotNull(result, "WineRecord retrieved from the database is null.");
            Assert.IsTrue(CompareWines(Expected, result), "Expected inserted wine to be the same");
        }

        [Test, Order(2)]
        public async Task WineRepository_UpdateShould()
        {
            Assert.IsTrue(InsertedId > 0, "Expected InsertedId to return an integer larger than 0.");

            // Change a few variables
            Expected.Name = "AlteredName!";
            Expected.Buy = 12.01m;
            Expected.Rating = 5;

            // Send the update to the database and check if rows affected is 1
            int rowsAffected = await DataAccess.WineRepo.Update(Expected);
            Assert.AreEqual(1, rowsAffected, $"Expected affected rows to be 1, it was {rowsAffected}");

            // Verify the results in database
            Wine result = await DataAccess.WineRepo.Get(InsertedId);
            Assert.IsTrue(CompareWines(Expected, result), "Retrieved record does not match updated record");
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
