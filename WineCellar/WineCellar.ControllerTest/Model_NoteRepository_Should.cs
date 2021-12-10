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
    public class Model_NoteRepository_Should
    {
        private IConfiguration? Configuration { get; set; }

        private NoteRecord Expected { get; set; } = new(0, "TestCountry");

        [SetUp]
        public void Setup()
        {
            Configuration = ConfigurationUtility.BuildConfiguration();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        [Test]
        public async Task NoteRepository_GetAllShould()
        {
            // Saving/creating the row in the database
            var result = (await DataAccess.NoteRepo.GetAll()).ToList();
            Assert.GreaterOrEqual(result.Count, 1);
        }

        [Test, Order(1)]
        public async Task NoteRepository_CreateShould()
        {
            // Insert a new note into the database
            int insertedId = await DataAccess.NoteRepo.Create(Expected);

            // Update the Id of the expected result
            Expected = Expected with { Id = insertedId };

            //Verify the record from database
            NoteRecord result = await DataAccess.NoteRepo.Get(Expected.Id);
            Assert.AreEqual(Expected, result);
        }

        [Test, Order(2)]
        public async Task NoteRepository_UpdateShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Change name of the CountryRecord
            Expected = Expected with { Name = "TestNoteAltered" };

            // Send the update to the database
            int rowsAffected = await DataAccess.NoteRepo.Update(Expected);
            Assert.AreEqual(1, rowsAffected);

            // Verify the record from database
            NoteRecord result = await DataAccess.NoteRepo.Get(Expected.Id);
            Assert.AreEqual(Expected, result);
        }

        [Test, Order(3)]
        public async Task NoteRepository_GetByWineShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Associate the wine with the created note
            await DataAccess.NoteRepo.AddWine(1, Expected.Id);

            // Retrieve all the notes for the wine and verify the note is added
            List<NoteRecord> result = (await DataAccess.NoteRepo.GetByWine(1)).ToList();
            Assert.Contains(Expected, result);

            // Remove the note from the wine
            await DataAccess.NoteRepo.RemoveWine(1, Expected.Id);

            // Verify the note is no longer associated with the wine
            result = (await DataAccess.NoteRepo.GetByWine(1)).ToList();
            foreach (NoteRecord record in result)
            {
                Assert.AreNotEqual(Expected, record);
            }
        }

        [Test, Order(4)]
        public async Task NoteRepository_DeleteShould()
        {
            Assert.IsTrue(Expected.Id > 0, "Expected InsertedId to return an integer larger than 0.");

            // Delete the record from the database
            await DataAccess.NoteRepo.Delete(Expected.Id);

            // Verify that the row cannot be retrieved
            Assert.ThrowsAsync<InvalidOperationException>(() => DataAccess.NoteRepo.Get(Expected.Id));
        }
    }
}
