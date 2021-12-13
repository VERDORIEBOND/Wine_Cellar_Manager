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
    public int HarvestYear { get; set; }
    public string[] Taste { get; set; } = new string[0];
    public double Alcohol { get; set; }
    public int Rating { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string[] StorageLocation { get; set; } = new string[0];
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
                wineEntry.HarvestYear = wine.Year;
                wineEntry.Stock = wine.Content;
                wineEntry.Alcohol = (double)wine.Alcohol;
                wineEntry.Type = wine.Type;
                wineEntry.TypeID = wine.TypeId;
                wineEntry.OriginCountry = wine.Country;
                wineEntry.CountryID = wine.CountryId;
                wineEntry.BuyPrice = (double)wine.Buy;
                wineEntry.SellPrice = (double)wine.Sell;

                string[] wineNotes = Array.Empty<string>();
                foreach (var note in await DataAccess.NoteRepo.GetByWine(wine.Id))
                {
                    wineNotes = wineNotes.Append(note.Name).ToArray();
                }
                wineEntry.Taste = wineNotes;

                string[] storageLocations = Array.Empty<string>();
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
