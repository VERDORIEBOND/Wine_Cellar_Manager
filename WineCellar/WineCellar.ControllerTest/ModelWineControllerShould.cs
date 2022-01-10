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
using Model;
using WineCellar.ControllerTest.Utilities;

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class ModelWineControllerShould
    {
        [Test, Order(1)]
        public async Task WineController_CreateShould()
        {
            //Data.GetAlWines unit test
        }
        [Test, Order(2)]
        public async Task WineController_GetAllCountriesShould()
        {
            //Data.GetAlWines unit test
        }
        [Test, Order(3)]
        public async Task WineController_GetAllTypesShould()
        {
            //Data.GetAlWines unit test
        }
        [Test, Order(4)]
        public async Task WineController_FilterShould()
        {
            //Data Filter method unit test
            var wineList = new List<IWineData>();
            var wineEntry = new WineData();
            wineEntry.Name = "TESTWINENAME";
            wineEntry.Age = 2000;
            wineEntry.Stock = 100;
            wineEntry.Type = "TESTTYPE";
            wineEntry.OriginCountry = "somewhere";
            wineEntry.BuyPrice = 100;
            wineEntry.SellPrice = 200;
            wineEntry.StorageLocation = new []{"A1.1", "A1.2", "A1.3"};
            wineEntry.Taste = new []{"sweet", "sour", "bitter"};
            wineList.Add(wineEntry);

            var notes = new List<string>();
            notes.Add("sweet");
            
            //var filteredList = Data.FilterWine(wineList, "Test", 150, 250, "TESTTYPE", "A1.1", 1999, 2001, notes, 5);
            //Assert.NotNull(filteredList);
        }
        [Test, Order(4)]
        public async Task WineController_DeleteWineShould()
        {
            //Data.GetAlWines unit test
        }
        [Test, Order(4)]
        public async Task WineController_AddStockShould()
        {
            //Data.GetAlWines unit test
        }
        [Test, Order(4)]
        public async Task WineController_RemoveStockShould()
        {
            //Data.GetAlWines unit test
        }
        
    }
}
