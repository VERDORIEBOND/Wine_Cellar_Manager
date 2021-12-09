using Microsoft.Extensions.Configuration;
using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.Model;

namespace Controller;

public static class ConfigurationAccess
{
    public static async void AddDatabase(DatabaseInformation information)
    {
        // Read the appsettings and convert it to a string
        string json = await File.ReadAllTextAsync(GetAppsettingsPath());

        // Load the appsettings.json as a JObject
        JObject appsettings = JObject.Parse(json);
        // Entry "Databases" is a keyvalue structure
        JArray? databases = (JArray?)appsettings["Databases"];

        // Add the object for databases into the json if it doesn't exist
        if (databases is null)
        {
            appsettings["Databases"] = new JArray();
            databases = (JArray?)appsettings["Databases"];
        }

        // Check if it exists and add an object to it
        if (databases is not null)
        {
            JObject databaseObj = new();
            databaseObj["Name"] = information.Name;
            databaseObj["Host"] = information.Host;
            databaseObj["Port"] = information.Port;
            databaseObj["User"] = information.User;
            databaseObj["Password"] = information.Password;
            databaseObj["Database"] = information.Database;

            databases.Add(databaseObj);
        }

        await File.WriteAllTextAsync(GetAppsettingsPath(), appsettings.ToString(Newtonsoft.Json.Formatting.Indented));
    }

    public static async Task<List<DatabaseInformation>> GetDatabasesAsync()
    {
        // Read the appsettings.json or appsettings.Development.json
        string json = await File.ReadAllTextAsync(GetAppsettingsPath());
        JObject appsettings = JObject.Parse(json);

        // List for all found databases
        List<DatabaseInformation> databases = new();

        JArray? jsonDatabases = (JArray?)appsettings["Databases"];
        if (jsonDatabases is not null)
        {
            // Turn the JArray into a list
            databases = jsonDatabases.ToObject<List<DatabaseInformation>>() ?? databases;
        }

        return databases;
    }

    private static string GetAppsettingsPath()
    {
        bool useDevelopment = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "appsettings.Development.json"));
        return useDevelopment ?
            Path.Join(Directory.GetCurrentDirectory(), "appsettings.Development.json")
            : Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
    }
}
