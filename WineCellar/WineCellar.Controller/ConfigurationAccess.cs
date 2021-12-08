using Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        JToken? databases = appsettings["Databases"];

        // Add the object for databases into the json if it doesn't exist
        if (databases is null)
        {
            appsettings["Databases"] = new JObject();
            databases = appsettings["Databases"];
        }

        // Check if it exists and add an object to it
        if (databases is not null && databases[information.Name] is not null)
        {
            JObject databaseObj = new();
            databaseObj["Host"] = information.Host;
            databaseObj["Port"] = information.Port;
            databaseObj["User"] = information.User;
            databaseObj["Password"] = information.Password;
            databaseObj["Database"] = information.Database;

            databases[information.Name] = databaseObj;
        }

        await File.WriteAllTextAsync(GetAppsettingsPath(), appsettings.ToString(Newtonsoft.Json.Formatting.Indented));
    }

    private static string GetAppsettingsPath()
    {
        bool useDevelopment = File.Exists(Path.Join(Directory.GetCurrentDirectory(), "appsettings.Development.json"));
        return useDevelopment ?
            Path.Join(Directory.GetCurrentDirectory(), "appsettings.Development.json")
            : Path.Join(Directory.GetCurrentDirectory(), "appsettings.json");
    }
}
