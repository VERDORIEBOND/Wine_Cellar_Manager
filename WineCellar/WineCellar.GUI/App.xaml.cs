using Controller;
using Controller.Repositories;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WineCellar.Views.DatabaseSetup;

namespace WineCellar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IConfiguration Configuration { get; private set; }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            // Builder is used for configuring the way configuration is retrieved
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

            // This creates the IConfiguration object
            Configuration = builder.Build();

            // Retrieve database information from appsettings
            List<DatabaseInformation> databases = new();
            foreach (IConfigurationSection db in Configuration.GetSection("Databases").GetChildren())
            {
                var dbInfo = db.Get<DatabaseInformation>();
                dbInfo.Name = db.Key;
                databases.Add(dbInfo);
            }

            // Temporarily disable shutdown on mainwindow close, as closing dialog would also cause it to shutdown
            ShutdownMode = ShutdownMode.OnExplicitShutdown;
            DatabaseSelectWindow selection = new(databases);
            bool? success = selection.ShowDialog();

            if (success is not null)
            {
                if ((bool)!success)
                    return;

                DatabaseInformation sdb = selection.GetSelectedDatabase();
                DataAccess.SetConnectionString(sdb.ConnectionString);

                MainWindow mainWindow = new();
                MainWindow = mainWindow;
                ShutdownMode = ShutdownMode.OnMainWindowClose;
                mainWindow.Show();
            }
            
        }
    }
}
