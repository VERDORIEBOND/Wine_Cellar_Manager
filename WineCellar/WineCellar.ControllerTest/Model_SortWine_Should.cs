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
using Model;

namespace WineCellar.ControllerTest
{
    [TestFixture]
    public class Model_SortWine_Should
    {
        private List<IWineData>? dataList;
        private WineData? wineData1;
        private WineData? wineData2;
        private bool descending;

        [SetUp]
        public void Setup()
        {
            dataList = new List<IWineData>();
            wineData1 = new WineData();
            wineData2 = new WineData();
            dataList.Add(wineData1);
            dataList.Add(wineData2);
        }

        [Test]
        public void SortAscendingName()
        {
            wineData1.Name = "Arend";
            wineData2.Name = "Berend";
            descending = false;

            dataList = Data.SortWine("Naam", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Name, wineData1.Name);
            Assert.AreEqual(dataList.ElementAt(1).Name, wineData2.Name);
        }

        [Test]
        public void SortDescendingName()
        {
            wineData1.Name = "Arend";
            wineData2.Name = "Berend";
            descending = true;

            dataList = Data.SortWine("Naam", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Name, wineData2.Name);
            Assert.AreEqual(dataList.ElementAt(1).Name, wineData1.Name);
        }

        [Test]
        public void SortAscendingSell()
        {
            wineData1.SellPrice = 1.9;
            wineData2.SellPrice = 2.1;
            descending = false;

            dataList = Data.SortWine("Verkoopprijs", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).SellPrice, wineData1.SellPrice);
            Assert.AreEqual(dataList.ElementAt(1).SellPrice, wineData2.SellPrice);
        }

        [Test]
        public void SortDescendingSell()
        {
            wineData1.SellPrice = 1.9;
            wineData2.SellPrice = 2.1;
            descending = true;

            dataList = Data.SortWine("Verkoopprijs", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).SellPrice, wineData2.SellPrice);
            Assert.AreEqual(dataList.ElementAt(1).SellPrice, wineData1.SellPrice);
        }

        [Test]
        public void SortAscendingBuy()
        {
            wineData1.BuyPrice = 1.9;
            wineData2.BuyPrice = 2.1;
            descending = false;

            dataList = Data.SortWine("Inkoopprijs", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).BuyPrice, wineData1.BuyPrice);
            Assert.AreEqual(dataList.ElementAt(1).BuyPrice, wineData2.BuyPrice);
        }

        [Test]
        public void SortDescendingBuy()
        {
            wineData1.BuyPrice = 1.9;
            wineData2.BuyPrice = 2.1;
            descending = true;

            dataList = Data.SortWine("Inkoopprijs", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).BuyPrice, wineData2.BuyPrice);
            Assert.AreEqual(dataList.ElementAt(1).BuyPrice, wineData1.BuyPrice);
        }
        [Test]
        public void SortAscendingCountry()
        {
            wineData1.OriginCountry = "Argentinië";
            wineData2.OriginCountry = "België";
            descending = false;

            dataList = Data.SortWine("Land van herkomst", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).OriginCountry, wineData1.OriginCountry);
            Assert.AreEqual(dataList.ElementAt(1).OriginCountry, wineData2.OriginCountry);
        }

        [Test]
        public void SortDescendingCountry()
        {
            wineData1.OriginCountry = "Argentinië";
            wineData2.OriginCountry = "België";
            descending = true;

            dataList = Data.SortWine("Land van herkomst", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).OriginCountry, wineData2.OriginCountry);
            Assert.AreEqual(dataList.ElementAt(1).OriginCountry, wineData1.OriginCountry);
        }
        [Test]
        public void SortAscendingType()
        {
            wineData1.Type = "Malbec";
            wineData2.Type = "Marcian";
            descending = false;

            dataList = Data.SortWine("Type wijn", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Type, wineData1.Type);
            Assert.AreEqual(dataList.ElementAt(1).Type, wineData2.Type);
        }

        [Test]
        public void SortDescendingType()
        {
            wineData1.Type = "Malbec";
            wineData2.Type = "Marcian";
            descending = true;

            dataList = Data.SortWine("Type wijn", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Type, wineData2.Type);
            Assert.AreEqual(dataList.ElementAt(1).Type, wineData1.Type);
        }
        [Test]
        public void SortAscendingJaartal()
        {
            wineData1.Age = 2001;
            wineData2.Age = 20020;
            descending = false;

            dataList = Data.SortWine("Jaartal", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Age, wineData1.Age);
            Assert.AreEqual(dataList.ElementAt(1).Age, wineData2.Age);
        }

        [Test]
        public void SortDescendingJaartal()
        {
            wineData1.Age = 2001;
            wineData2.Age = 2002;
            descending = true;

            dataList = Data.SortWine("Jaartal", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Age, wineData2.Age);
            Assert.AreEqual(dataList.ElementAt(1).Age, wineData1.Age);
        }
        [Test]
        public void SortAscendingVoorraad()
        {
            wineData1.Stock = 1;
            wineData2.Stock = 2;
            descending = false;

            dataList = Data.SortWine("Voorraad", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Stock, wineData1.Stock);
            Assert.AreEqual(dataList.ElementAt(1).Stock, wineData2.Stock);
        }

        [Test]
        public void SortDescendingVoorraad()
        {
            wineData1.Stock = 1;
            wineData2.Stock = 2;
            descending = true;

            dataList = Data.SortWine("Voorraad", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Stock, wineData2.Stock);
            Assert.AreEqual(dataList.ElementAt(1).Stock, wineData1.Stock);
        }
        [Test]
        public void SortAscendingRating()
        {
            wineData1.Rating = 1;
            wineData2.Rating = 2;
            descending = false;

            dataList = Data.SortWine("Rating", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Rating, wineData1.Rating);
            Assert.AreEqual(dataList.ElementAt(1).Rating, wineData2.Rating);
        }

        [Test]
        public void SortDescendingRating()
        {
            wineData1.Rating = 1;
            wineData2.Rating = 2;
            descending = true;

            dataList = Data.SortWine("Rating", dataList, descending);

            Assert.AreEqual(dataList.ElementAt(0).Rating, wineData2.Rating);
            Assert.AreEqual(dataList.ElementAt(1).Rating, wineData1.Rating);
        }
    }
}
