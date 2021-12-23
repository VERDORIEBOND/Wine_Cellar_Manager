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

                var wineNotes = Array.Empty<string>();
                foreach (var note in await DataAccess.NoteRepo.GetByWine(wine.Id))
                {
                    wineNotes = wineNotes.Append(note.Name).ToArray();
                }
                wineEntry.Taste = wineNotes;

                var storageLocations = Array.Empty<string>();
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


        public static async void Update(WineData wine)
        {
            Wine newWine = new(wine.ID,
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
            var wineRepo = await DataAccess.WineRepo.Update(newWine);
        }

        public static async void Create(WineData wine)
        {
            Wine newWine = new(0,
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

        public static List<IWineData> FilterWineName(List<IWineData> wineDatas, string? name)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if (wine.Name.ToLower().Contains(name?.ToLower() ?? string.Empty))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWinePrice(List<IWineData> wineDatas, double priceFrom, double priceTo)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if ((wine.SellPrice >= priceFrom && wine.SellPrice <= priceTo))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWineType(List<IWineData> wineDatas, string wineType)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if (wine.Type.ToLower().Contains(wineType?.ToLower() ?? string.Empty))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWineStorage(List<IWineData> wineDatas, string storageLocation)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if (wine.StorageLocation.Contains(storageLocation))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWineAge(List<IWineData> wineDatas, double ageFrom, double ageTo)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if ((wine.Age >= ageFrom && wine.Age <= ageTo))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWineRating(List<IWineData> wineDatas, int rating)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                if ((wine.Rating == rating))
                {
                    var wineEntry = new WineData();
                    wineEntry.ID = wine.ID;
                    wineEntry.Name = wine.Name;
                    wineEntry.Age = wine.Age;
                    wineEntry.Stock = wine.Stock;
                    wineEntry.Type = wine.Type;
                    wineEntry.OriginCountry = wine.OriginCountry;
                    wineEntry.BuyPrice = wine.BuyPrice;
                    wineEntry.SellPrice = wine.SellPrice;
                    wineEntry.StorageLocation = wine.StorageLocation;
                    wineEntry.Taste = wine.Taste;

                    filteredWine.Add(wineEntry);
                }
            }
            return filteredWine;
        }

        public static List<IWineData> FilterWineTaste(List<IWineData> wineDatas, List<string> notes)
        {
            var filteredWine = new List<IWineData>();

            foreach (var wine in wineDatas)
            {
                foreach (var item in notes)
                {
                    if ((wine.Taste.ToList().All(notes.Contains) && wine.Taste.Length > 0) || (notes.Count < 2 && wine.Taste.ToList().Contains(item)))
                    {
                        var wineEntry = new WineData();
                        wineEntry.ID = wine.ID;
                        wineEntry.Name = wine.Name;
                        wineEntry.Age = wine.Age;
                        wineEntry.Stock = wine.Stock;
                        wineEntry.Type = wine.Type;
                        wineEntry.OriginCountry = wine.OriginCountry;
                        wineEntry.BuyPrice = wine.BuyPrice;
                        wineEntry.SellPrice = wine.SellPrice;
                        wineEntry.StorageLocation = wine.StorageLocation;
                        wineEntry.Taste = wine.Taste;

                        filteredWine.Add(wineEntry);
                    }
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

                Wine wineRecord = new(wine.Id, wine.Name, wine.Buy, wine.Sell, wine.TypeId, wine.Type, wine.CountryId, wine.Country, wine.Picture, wine.Year, stock, wine.Alcohol, wine.Rating, wine.Description);
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

                Wine wineRecord = new(wine.Id, wine.Name, wine.Buy, wine.Sell, wine.TypeId, wine.Type, wine.CountryId, wine.Country, wine.Picture, wine.Year, stock, wine.Alcohol, wine.Rating, wine.Description);
                await DataAccess.WineRepo.Update(wineRecord);
            }

            return stock;
        }

        public static List<IWineData> SortWine(string sort, List<IWineData> sortedlist, bool descending)
        {
            switch (sort)
            {
                case "Naam":
                    sortedlist.Sort((x, y) => x.Name.CompareTo(y.Name));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Verkoopprijs":
                    sortedlist.Sort((x, y) => x.SellPrice.CompareTo(y.SellPrice));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Inkoopprijs":
                    sortedlist.Sort((x, y) => x.BuyPrice.CompareTo(y.BuyPrice));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Type wijn":
                    sortedlist.Sort((x, y) => x.Type.CompareTo(y.Type));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Land van herkomst":
                    sortedlist.Sort((x, y) => x.OriginCountry.CompareTo(y.OriginCountry));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Jaartal":
                    sortedlist.Sort((x, y) => x.Age.CompareTo(y.Age));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Voorraad":
                    sortedlist.Sort((x, y) => x.Stock.CompareTo(y.Stock));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                case "Rating":
                    sortedlist.Sort((x, y) => x.Rating.CompareTo(y.Rating));
                    if (descending) { sortedlist.Reverse(); }

                    return sortedlist;
                default:
                    return sortedlist;
            }
        }

        public static async Task<WineData> GetWine(int ID)
        {
            var data = await DataAccess.WineRepo.Get(ID);
            WineData wineEntry = new WineData();

            wineEntry.ID = data.Id;
            wineEntry.Name = data.Name;
            wineEntry.Description = data.Description;
            wineEntry.Rating = data.Rating;
            wineEntry.Age = data.Year;
            wineEntry.Stock = data.Content;
            wineEntry.Alcohol = data.Alcohol;
            wineEntry.Type = data.Type;
            wineEntry.TypeID = data.TypeId;
            wineEntry.OriginCountry = data.Country;
            wineEntry.CountryID = data.CountryId;
            wineEntry.BuyPrice = (double)data.Buy;
            wineEntry.SellPrice = (double)data.Sell;
            wineEntry.Picture = data.Picture;

            var wineNotes = Array.Empty<string>();
            foreach (var note in await DataAccess.NoteRepo.GetByWine(data.Id))
            {
                wineNotes = wineNotes.Append(note.Name).ToArray();
            }
            wineEntry.Taste = wineNotes;

            var storageLocations = Array.Empty<string>();
            foreach (var location in await DataAccess.LocationRepo.GetByWine(data.Id))
            {
                if (location.IdWine == data.Id)
                {
                    storageLocations = storageLocations.Append(location.Shelf + location.Row + "." + location.Col).ToArray();
                }
            }
            wineEntry.StorageLocation = storageLocations;

            return wineEntry;
        }
    }
}
