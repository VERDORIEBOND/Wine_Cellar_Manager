using Controller;
using Model;
using System.Linq;
using WineCellar.DummyData;
using WineCellar.Model;

Console.WriteLine("Hello, this is a simple application to generate a lot of records for testing purposes.");

Console.WriteLine("\nPlease answer the following database related questions:");
Console.Write("Hostname: ");
string? inputHost = Console.ReadLine();
if (string.IsNullOrEmpty(inputHost))
{
    Console.WriteLine("No hostname provided, exiting application...");
    return;
}

Console.Write("Port: ");
string? inputPort = Console.ReadLine();
if (string.IsNullOrEmpty(inputPort))
{
    Console.WriteLine("No port provided, exiting application...");
    return;
}

Console.Write("User: ");
string? inputUser = Console.ReadLine();
if (string.IsNullOrEmpty(inputUser))
{
    Console.WriteLine("No username provided, exiting application...");
    return;
}

Console.Write("Password: ");
string? inputPassword = Console.ReadLine();
if (string.IsNullOrEmpty(inputPassword))
{
    Console.WriteLine("No password provided, exiting application...");
    return;
}

Console.Write("Database: ");
string? inputDatabase = Console.ReadLine();
if (string.IsNullOrEmpty(inputDatabase))
{
    Console.WriteLine("No database provided, exiting application...");
    return;
}

Console.WriteLine();

DatabaseInformation databaseInfo = new("", inputHost, inputPort, inputUser, inputPassword, inputDatabase);

if (!(await DataAccess.CheckConnectionFor(databaseInfo.ConnectionString)))
{
    Console.WriteLine("Couldn't connect to the following database connectionstring:");
    Console.WriteLine(databaseInfo.ConnectionString);
    Console.WriteLine("Exiting application.");

    return;
}

DataAccess.SetConnectionString(databaseInfo.ConnectionString);

string[] countriesInDb = (await DataAccess.CountryRepo.GetAll()).Select(c => c.Name).ToArray();
foreach (string country in PropertyOptions.Countries)
{
    if (!countriesInDb.Contains(country))
    {
        await DataAccess.CountryRepo.Create(new CountryRecord(0, country));
    }
}

string[] typesInDb = (await DataAccess.TypeRepo.GetAll()).Select(c => c.Name).ToArray();
foreach (string type in PropertyOptions.Types)
{
    if (!typesInDb.Contains(type))
    {
        await DataAccess.TypeRepo.Create(new TypeRecord(0, type));
    }
}

string[] notesInDb = (await DataAccess.NoteRepo.GetAll()).Select(c => c.Name).ToArray();
foreach (string note in PropertyOptions.Notes)
{
    if (!typesInDb.Contains(note))
    {
        await DataAccess.NoteRepo.Create(new NoteRecord(0, note));
    }
}

int[] countries = (await DataAccess.CountryRepo.GetAll()).Select(a => a.Id).ToArray();
int[] types = (await DataAccess.TypeRepo.GetAll()).Select(a => a.Id).ToArray();
int[] notes = (await DataAccess.NoteRepo.GetAll()).Select(a => a.Id).ToArray();

Console .Write("How many wines should be added: ");
int amountWines = 0;
bool success = int.TryParse(Console.ReadLine(), out amountWines);

if (!success)
    return;

Random rand = new(1305273849);

List<int> createdIds = new(amountWines);

for (int i = 0; i < amountWines; i++)
{
    WineRecord newWine = new(0,
        PropertyOptions.WineNames[rand.Next(0, PropertyOptions.WineNames.Length)],
        new decimal(rand.NextDouble() * 100),
        new decimal(rand.NextDouble() * 100),
        types[rand.Next(0, types.Length)],
        string.Empty,
        countries[rand.Next(0, countries.Length)],
        string.Empty,
        null,
        rand.Next(1975, 2021),
        rand.Next(5, 15) * 100,
        new decimal(rand.NextDouble() * 20),
        rand.Next(1, 5),
        PropertyOptions.WineDescriptions[rand.Next(0, PropertyOptions.WineDescriptions.Length)]
        );

    int created = await DataAccess.WineRepo.Create(newWine);
    createdIds.Add(created);
}

foreach (int id in createdIds)
{
    int amtNotes = rand.Next(1, 3);
    int amtLocations = rand.Next(1, 15);

    HashSet<int> occupiedNotes = new(amtNotes);
    HashSet<StorageLocationRecord> occupiedLocations = new(amtLocations);

    for (int i = 0; i < amtNotes; i++)
    {
        int selectedId = notes[rand.Next(0, notes.Length)];

        while (occupiedNotes.Contains(selectedId))
        {
            selectedId = notes[rand.Next(0, notes.Length)];
        }

        occupiedNotes.Add(selectedId);
        await DataAccess.NoteRepo.AddWine(id, selectedId);
    }

    for (int i = 0; i < amtLocations; i++)
    {
        StorageLocationRecord selectedLocation = new StorageLocationRecord(
            id,
            ((char)rand.Next(65, 90)).ToString(),
            rand.Next(1, 100),
            rand.Next(1, 100));

        while (occupiedLocations.Contains(selectedLocation))
        {
            selectedLocation = new StorageLocationRecord(
            id,
            ((char)rand.Next(0, 25)).ToString(),
            rand.Next(1, 100),
            rand.Next(1, 100));
        }

        occupiedLocations.Add(selectedLocation);
        await DataAccess.LocationRepo.Create(selectedLocation);
    }
}
