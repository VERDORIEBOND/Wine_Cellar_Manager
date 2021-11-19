using System.Collections.Generic;
using Model;

class WineData : IWineData
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Type { get; set; }
    public string OriginCountry { get; set; }
    public int Stock { get; set; }
    public string[] StorageLocation { get; set; }
    public double BuyPrice { get; set; }
    public double SellPrice { get; set; }
}


namespace Controller
{
    public class Data
    {
        public static List<IWineData> GetWineData()
        {
            return new List<IWineData>
            {
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Catena Malbec", Age = 2019, Type = "Malbec", OriginCountry = "Argentinia", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99}
            };
        }
    }
}