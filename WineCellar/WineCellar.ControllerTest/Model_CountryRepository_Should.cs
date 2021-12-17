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
    public class Model_CountryRepository_Should
    {
        private IConfiguration? Configuration { get; set; }

        private Country Expected { get; set; } = new(0, "TestCountry");

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        private static bool CompareCountries(Country country1, Country country2)
        {
            return country1.Id == country2.Id
                && country1.Name.Equals(country2.Name);
        }

        [Test]
        public async Task CountryRepository_GetAllShould()
        {
            // Saving/creating the row in the database
            var result = (await DataAccess.CountryRepo.GetAll()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test, Order(1)]
        public async Task CountryRepository_CreateShould()
        {
            // Insert a new country into the database
            int insertedId = await DataAccess.CountryRepo.Create(Expected);

            // Update the Id of the expected result
            Expected.Id = insertedId;

            //Verify the record from database
            Country result = await DataAccess.CountryRepo.Get(Expected.Id);
            Assert.IsTrue(CompareCountries(Expected, result), "Expected inserted country to be the same");
        }

        [Test, Order(2)]
        public async Task CountryRepository_UpdateShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Change name of the CountryRecord
            Expected.Name = "TestCountryAltered";

            // Send the update to the database
            int rowsAffected = await DataAccess.CountryRepo.Update(Expected);
            Assert.AreEqual(1, rowsAffected);

            // Verify the record from database
            Country result = await DataAccess.CountryRepo.Get(Expected.Id);
            Assert.IsTrue(CompareCountries(Expected, result), "Expected inserted type to be the same");
        }

        [Test, Order(3)]
        public async Task CountryRepository_DeleteShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Delete the record from the database
            await DataAccess.CountryRepo.Delete(Expected.Id);

            // Verify that the row cannot be retrieved
            Assert.ThrowsAsync<InvalidOperationException>(() => DataAccess.CountryRepo.Get(Expected.Id));
        }
    }
}
