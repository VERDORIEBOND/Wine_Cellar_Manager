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
    public string OriginCountry { get; set; } = string.Empty;
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
        public static List<IWineData> GetWineData()
        {
            return new List<IWineData>
            {
                new WineData {ID = 0, Name = "Catena Malbec", HarvestYear = 2019, Rating = 8, Type = "Malbec", Alcohol = 5, OriginCountry = "Argentinia", Stock = 7, Taste = new string[] { "Kruidig, Geconcentreerd en Bramen"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99, Description = "Door de druiven op zeer grote hoogte te planten en goed te klonen, kreeg Nicolas Catena de beste resultaten met de malbecdruif.  Na deze ontdekking hebben veel andere Argentijnse wijnmakers zijn voorbeeld gevolgd en inmiddels is de malbec de nationale druif. Een schitterende Argentijnse Malbec, helemaal gemaakt volgens het boekje."},
                new WineData {ID = 1, Name = "Faustino V Rioja Reserva", HarvestYear = 2016, Rating = 10,  Type = "tempranillo", Alcohol = 8, OriginCountry = "Spanje", Stock = 2, Taste = new string[] { "Donker Bessenfruit, Chocolade en Specerijen"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 11.99, SellPrice = 23.99, Description = "Bij Faustino heet de Gran Reserva Faustino I en de ‘normale’ Reserva heet Faustino V. Nog altijd moet de wijn daarvoor eerst 18 maanden rijpen op Amerikaanse eikenhouten vaten. Om vervolgens nog meer dan een jaar op fles door te rijpen in de kelders van de bodega. Het is een blend van tempranillo met 8% mazuelo. Deze Rioja is complex en intens met donker bessenfruit, vanille, chocola, specerijen en sigarenkistjes. Klassieke begeleider van alle soorten lamsvlees. Ook heerlijk bij Jamón Ibérico"},
                new WineData {ID = 2, Name = "Cantina di Verona Valpolicella Ripasso", HarvestYear = 2019, Rating = 6, Type = "Corvina", Alcohol = 6, OriginCountry = "Italië", Stock = 12, Taste = new string[] { "Intens en Zwart Kersenfruit"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 10.65, SellPrice = 23.99, Description = "Ripasso vergist voor een tweede keer op de droesem van Amarone wijn. Hierdoor krijgt de wijn extra veel smaak."},
                new WineData {ID = 3, Name = "Mucho Más Tinto", HarvestYear = 0, Rating = 5, Type = "Tempranillo", Alcohol = 7, OriginCountry = "Spanje", Stock = 23, Taste = new string[] {"Specerijen, Zacht en Kers" }, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.98, SellPrice = 23.99, Description = "Bij Felix Solis vroegen ze aan hun wijnmakers om helemaal los te gaan. Vergeet de regels en maak wat je wilt. Een wijn die je zelf het liefste drinkt. Het resultaat is Mucho Más. Een wijn gemaakt van druiven uit Toro, La Mancha en Valdepeñas. Het is een kort op hout gerijpte blend van garnacha, aangevuld met tempranillo. Uiteraard moet de wijn worden gedeclasseerd tot het laagste niveau: een landwijn. Maar de smaak is geweldig: zwarte kersen, bramen, specerijen, zijdezachte tannines en een lange afdronk."},
                new WineData {ID = 4, Name = "Valdivieso Merlot", HarvestYear = 2020, Rating = 8, Type = "Merlot", Alcohol = 6, OriginCountry = "Chili", Stock = 3, Taste = new string[] { "Pruimen, Bessen en Houtrijping"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99, Description = "Bij de wijnen van Valdivieso zorgt de Nieuw-Zeelandse wijnmaker Brett David Jackson er altijd voor dat het fruit in de smaak de hoofdrol heeft. In het geval van deze merlot is dat pruimen, aardbei en bessen. Daarnaast proef je specerijen, koffie, chocola en een lichte rokerigheid. Deze merlot is een echte crowd pleaser. Ideaal om te schenken bij borrels of op feestjes. Hij gaat ook verrassend goed met rood vlees, pasta’s, stoofschotels of gegrilde kip. "},
                new WineData {ID = 5, Name = "Bruce Jack", HarvestYear = 2019, Rating = 9, Type = "Sauvignon Blanc", Alcohol = 7, OriginCountry = "Zuid-Africa", Stock = 7, Taste = new string[] { "Groene Appels en Citrus"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 6.99, SellPrice = 23.99, Description = "Bruce Jack maakt onder zijn eigen naam wijnen in Zuid-Afrika. Van druiven uit de Western Cape maakt hij deze Sauvignon Blanc. De wijn wordt vergeleken met een Pouilly-Fumé, maar dan met de toevoeging van wat steenfruit zoals perzik, abrikoos en nectarine in de smaak. Daarnaast is hij levendig en fris met groene appels en citrusfruit. Schenk deze heerlijke witte wijn in combinatie met groene salades, bij asperges, bij mosselen of andere gerechten met vis of schaal- en schelpdieren. "},
                new WineData {ID = 6, Name = "Alamos Merlot", HarvestYear = 2020, Rating = 5, Type = "Merlot", Alcohol = 8, OriginCountry = "Argentinia", Stock = 31, Taste = new string[] {"Zoete Pruimen en Fluweelzacht" }, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 6.59, SellPrice = 23.99, Description = "Een rode wijn met een fluweelzachte smaak van zoete pruimen en zwarte kersen. "},
                new WineData {ID = 7, Name = "Domaine Andau", HarvestYear = 2019, Rating = 7, Type = "Grüner Veltliner", Alcohol = 12, OriginCountry = "Oostenrijk", Stock = 3, Taste = new string[] { "Elegant en Iets Kruidig"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 7.99, SellPrice = 23.99, Description = "In de geur van deze super frisse Grüner Veltiner ruik je pitten van perziken en een beetje een kruidige geur van witte peper. De smaak is elegant smaak met mooie, verfijnde zuren."},
                new WineData {ID = 8, Name = "La Palma", HarvestYear = 2019, Rating = 4, Type = "Chardonnay", Alcohol = 2, OriginCountry = "Chili", Stock = 2, Taste = new string[] { "Soepel, Zacht en Tropisch Fruit"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99, Description = "Carmenere is de nationale druif van Chili. Oorspronkelijk komt hij uit Bordeaux, maar daar staat hij bijna niet meer aangeplant. Sterker nog, men dacht dat hij was uitgestorven. Totdat de Chilenen hem herontdekten en er achter kwamen dat de omstandigheden in Chili ideaal zijn voor de aanplant van deze druif. De wijngaarden van La Palma profiteren van grote verschillen tussen dag en nacht temperatuur wat zorgt voor een hoge kwaliteit. In deze carmenere proef je rode bessen, pruimen, bramen en cassis. Een toegankelijke wijn, geschikt voor ieder moment."},
                new WineData {ID = 9, Name = "La Palma", HarvestYear = 2020, Rating = 2, Type = "Carmenère", Alcohol = 80, OriginCountry = "Chili", Stock = 53, Taste = new string[] { "Bes, Pruim en Braam"}, StorageLocation = new []{"A3.6","A18.6","B3.20","B3.21","B3.22","B3.23","B3.24","C2.2"}, BuyPrice = 5.99, SellPrice = 23.99, Description = "De La Palma wijnen worden gemaakt door het Chileense wijnbedrijf Viña la Rosa. Alle wijnen van Viña la Rosa worden duurzaam geproduceerd met respect voor werknemers, leveranciers en nabijgelegen gemeenschap. In deze 100% chardonnay proef je de smaak van tropisch fruit. Denk aan ananas, banaan en een vleugje limoen. Daarnaast ontdek je perzik, gele appel en geroosterde hazelnoten. Hij is rijk, vol en romig. Juist daarom lekker bij gegrilde kip, gerookte zalm of varkenshaas met champignonroomsaus."}
            };
        }

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
                wineEntry.OriginCountry = wine.Country;
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
