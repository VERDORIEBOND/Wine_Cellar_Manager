using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using Model;
using WineCellar.Model;

public class WineData : IWineData
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
    public string Type { get; set; } = string.Empty;
    public int TypeID { get; set; }
    public string OriginCountry { get; set; } = string.Empty;
    public int CountryID { get; set; }
    public int Age { get; set; }
    public string[] Taste { get; set; } = new string[0];
    public int Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Contents { get; set; }
    public byte[]? Picture { get; set; } = null;
    public int Country { get; set; }
    public int Stock { get; set; }
    public string[] StorageLocation { get; set; } = Array.Empty<string>();
    public decimal Alcohol { get; set; }
    public string[] Notes { get; set; } = Array.Empty<string>();
}

namespace Controller
{
    public class Data
    {
        //Get all the wine data from the database
        public static async Task<List<IWineData>> GetAllWines()
        {
            var wineRepo = await DataAccess.WineRepo.GetAll();

            //convert the result to a list of wine data
            var wineData = new List<IWineData>();

            foreach (var wine in wineRepo)
            {
                WineData wineEntry = new WineData();

                wineEntry.ID = wine.Id;
                wineEntry.Name = wine.Name;
                wineEntry.Description = wine.Description;
                wineEntry.Rating = wine.Rating;
                wineEntry.Age = wine.Year;
                wineEntry.Stock = wine.Content;
                wineEntry.Alcohol = wine.Alcohol;
                wineEntry.Type = wine.Type;
                wineEntry.TypeID = wine.TypeId;
                wineEntry.OriginCountry = wine.Country;
                wineEntry.CountryID = wine.CountryId;
                wineEntry.BuyPrice = (double)wine.Buy;
                wineEntry.SellPrice = (double)wine.Sell;
                wineEntry.Picture = wine.Picture;

                var storageLocations = new string[] {};
                foreach (var location in await DataAccess.LocationRepo.GetByWine(wine.Id))
                {
                    //create array and add the location to it
                    if (location.IdWine == wine.Id)
                    {
                        storageLocations = storageLocations.Append(location.Shelf + location.Row + "." + location.Col).ToArray();
                    }
                }
                wineEntry.StorageLocation = storageLocations;

                wineData.Add(wineEntry);
            }

            return wineData;
        }

      
        public static async void Create(WineData wine)
        {
            WineRecord newWine = new(0,
                wine.Name,
                Convert.ToDecimal(wine.BuyPrice),
                Convert.ToDecimal(wine.SellPrice),
                wine.TypeID,
                wine.Type,
                wine.Country,
                string.Empty,
                wine.Picture,
                wine.Age,
                wine.Contents,
                wine.Alcohol,
                wine.Rating,
                wine.Description
            );
            var wineRepo = await DataAccess.WineRepo.Create(newWine);
        }

        public static async Task<Dictionary<string, string>> GetAllCountries()
        {
            Dictionary<string, string> Countries = new Dictionary<string, string>();

            foreach (Country country in await DataAccess.CountryRepo.GetAll())
            {
                Countries.Add(country.Id.ToString(), country.Name);
            }
            return Countries;
        }   
        public static async Task<Dictionary<string, string>> GetAllTypes()
        {
            Dictionary<string, string> Types = new Dictionary<string, string>();

            foreach (WineType type in await DataAccess.TypeRepo.GetAll())
            {
                Types.Add(type.Id.ToString(), type.Name);
            }
            return Types;
        }

        public static List<IWineData> FilterWine(List<IWineData> wineDatas, string? name, double priceFrom, double priceTo, string wineType, 
            string storageLocation, double ageFrom, double ageTo, List<string> tastingNotes, int rating)
        {
            var filteredWine = new List<IWineData>();
            foreach (var wine in wineDatas)
            { 
                if (wine.Name.ToLower().Contains(name?.ToLower() ?? string.Empty) &&
                    wine.SellPrice >= priceFrom && 
                    wine.SellPrice <= priceTo && 
                    wine.Type.ToLower().Contains(wineType?.ToLower() ?? string.Empty) && 
                    wine.Age >= ageFrom && 
                    wine.Age <= ageTo) 
                {
                    var wineEntry = new WineData();
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }
        public static async Task DeleteWine(int wineId)
        {
            await DataAccess.LocationRepo.DeleteAllByWine(wineId);
            await DataAccess.NoteRepo.RemoveByWineId(wineId);

            await DataAccess.WineRepo.Delete(wineId);
        }

        public static async Task<int> Add_Stock(int wineId)
        {
            var wine = await DataAccess.WineRepo.Get(wineId);
            int stock = wine.Content;

            if (stock >= 0)
            {
                stock++;

                WineRecord wineRecord = new WineRecord(wine.Id, wine.Name, wine.Buy, wine.Sell, wine.TypeId, wine.Type, wine.CountryId, wine.Country, wine.Picture, wine.Year, stock, wine.Alcohol, wine.Rating, wine.Description);
                await DataAccess.WineRepo.Update(wineRecord);
            }

            return stock;
        }

        public static async Task<int> Remove_Stock(int wineId)
        {
            var wine = await DataAccess.WineRepo.Get(wineId);
            int stock = wine.Content;

            if (stock > 0)
            {
                stock--;

                WineRecord wineRecord = new WineRecord(wine.Id, wine.Name, wine.Buy, wine.Sell, wine.TypeId, wine.Type, wine.CountryId, wine.Country, wine.Picture, wine.Year, stock, wine.Alcohol, wine.Rating, wine.Description);
                await DataAccess.WineRepo.Update(wineRecord);
            }

            return stock;
        }
    }
}
