using Controller;
using Microsoft.Extensions.Configuration;
using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class Model_WineRepository_Should
    {
        private IConfiguration? Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            // Builder is used for configuring the way configuration is retrieved
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            // This creates the IConfiguration object
            Configuration = builder.Build();

            // DataAccess requires the configuration to create SqlConnections
            DataAccess.SetConfiguration(Configuration);
        }

        [Test]
        public async Task WineRepository_CreateShould()
        {
            WineRecord expected = new(0, "TestWine", 12.10m, 13.20m, 1, 1, "empty", 2001, 700, 12.50m, 3, "Beschrijving");

            int id = await DataAccess.WineRepo.Create(expected);
            Assert.IsTrue(id > 0, "Expected Create to return an integer larger than 0.");

            expected = expected with { Id = id };
            WineRecord result = await DataAccess.WineRepo.Get(id);
            Assert.IsNotNull(result, "WineRecord retrieved from the database is null.");
            Assert.AreEqual(expected, result);
        }
    }
}
