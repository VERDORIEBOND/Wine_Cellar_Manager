using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WineCellar.ControllerTest.Utilities;

public static class ConfigurationUtility
{
    public static IConfiguration BuildConfiguration()
    {
        // Builder is used for configuring the way configuration is retrieved
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

        // This creates the IConfiguration object
        return builder.Build();
    }
}
