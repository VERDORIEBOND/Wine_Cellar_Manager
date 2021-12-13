using System.Collections.Generic;
using System.Diagnostics;
using Model;
using WineCellar.Model;

public class WineData : IWineData
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Type { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string[] StorageLocation { get; set; } = Array.Empty<string>();
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
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
                var wineEntry = new WineData();
                wineEntry.Name = wine.Name;
                wineEntry.Age = wine.Year;
                wineEntry.Stock = wine.Content;
                wineEntry.Type = wine.Type;
                wineEntry.OriginCountry = wine.Country;
                wineEntry.BuyPrice = (double)wine.Buy;
                wineEntry.SellPrice = (double)wine.Sell;

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

        public static List<IWineData> FilterWine(List<IWineData> wineDatas, string? name, double priceFrom, double priceTo, string wineType, string storageLocation, double ageFrom, double ageTo, List<string> tastingNotes, int rating)
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
    }
}