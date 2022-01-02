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
    public class Model_TypeRepository_Should
    {
        private IConfiguration? Configuration { get; set; }

        private WineType Expected { get; set; } = new(0, "TestType");

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        private static bool CompareTypes(WineType note1, WineType note2)
        {
            return note1.Id == note2.Id
                && note1.Name.Equals(note2.Name);
        }

        [Test]
        public async Task TypeRepository_GetAllShould()
        {
            // Saving/creating the row in the database, also store Id locally for other tests
            var result = (await DataAccess.TypeRepo.GetAll()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test, Order(1)]
        public async Task TypeRepository_CreateShould()
        {
            // Insert a new type into the database
            int insertedId = await DataAccess.TypeRepo.Create(Expected);

            // Update the Id of the expected result
            Expected.Id = insertedId;

            //Verify the record from database
            WineType result = await DataAccess.TypeRepo.Get(Expected.Id);
            Assert.IsTrue(CompareTypes(Expected, result), "Expected inserted type to be the same");
        }

        [Test, Order(2)]
        public async Task TypeRepository_UpdateShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Change name of the TypeRecord
            Expected.Name = "TestTypeAltered";

            // Send the update to the database
            int rowsAffected = await DataAccess.TypeRepo.Update(Expected);
            Assert.AreEqual(1, rowsAffected);

            // Verify the record from database
            WineType result = await DataAccess.TypeRepo.Get(Expected.Id);
            Assert.IsTrue(CompareTypes(Expected, result), "Expected inserted type to be the same");
        }

        [Test, Order(3)]
        public async Task TypeRepository_DeleteShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Delete the record from the database
            await DataAccess.TypeRepo.Delete(Expected.Id);

            // Verify that the row cannot be retrieved
            Assert.ThrowsAsync<InvalidOperationException>(() => DataAccess.TypeRepo.Get(Expected.Id));
        }
    }
}
