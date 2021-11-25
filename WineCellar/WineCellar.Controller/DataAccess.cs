using Controller.Repositories;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Controller;

public static class DataAccess
{
    public static IConfiguration? Configuration { get; private set; }
    public static WineRepository WineRepo { get; } = new();
    public static CountryRepository CountryRepo { get; } = new();

    public static void SetConfiguration(IConfiguration config)
    {
        Configuration = config;
    }

    public static IDbConnection GetConnection
    {
        get
        {
            return new SqlConnection(Configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
