using System.Collections.Generic;
using Model;

public class WineData : IWineData
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Type { get; set; } = string.Empty;
    public string OriginCountry { get; set; } = string.Empty;
    public int Stock { get; set; }
    public string[] StorageLocation { get; set; } = new string[0];
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
                new WineData {Name = "Faustino V Rioja Reserva", Age = 2016, Type = "tempranillo", OriginCountry = "Spanje", Stock = 2, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99},
                new WineData {Name = "Cantina di Verona Valpolicella Ripasso", Age = 2019, Type = "Corvina", OriginCountry = "Italië", Stock = 12, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 10.65, SellPrice = 23.99},
                new WineData {Name = "Mucho Más Tinto", Age = 0, Type = "Tempranillo", OriginCountry = "Spanje", Stock = 23, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.98, SellPrice = 23.99},
                new WineData {Name = "Valdivieso Merlot", Age = 2020, Type = "Merlot", OriginCountry = "Chili", Stock = 3, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99},
                new WineData {Name = "Bruce Jack", Age = 2019, Type = "Sauvignon Blanc", OriginCountry = "Zuid-Africa", Stock = 7, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 6.99, SellPrice = 23.99},
                new WineData {Name = "Alamos Merlot", Age = 2020, Type = "Merlot", OriginCountry = "Argentinia", Stock = 31, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 6.59, SellPrice = 23.99},
                new WineData {Name = "Domaine Andau", Age = 2019, Type = "Grüner Veltliner", OriginCountry = "Oostenrijk", Stock = 3, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 7.99, SellPrice = 23.99},
                new WineData {Name = "La Palma", Age = 2019, Type = "Chardonnay", OriginCountry = "Chili", Stock = 2, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99},
                new WineData {Name = "La Palma", Age = 2020, Type = "Carmenère", OriginCountry = "Chili", Stock = 53, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99}
            };
        }
    }
}