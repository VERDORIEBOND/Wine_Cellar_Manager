using Controller.Repositories;
using Microsoft.Extensions.Configuration;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Controller;

public static class DataAccess
{
    private static string? _ConnectionString { get; set; }
    public static IConfiguration? Configuration { get; private set; }
    public static WineRepository WineRepo { get; } = new();
    public static CountryRepository CountryRepo { get; } = new();
    public static TypeRepository TypeRepo { get; } = new();
    public static StorageLocationRepository LocationRepo { get; } = new();
    public static NoteRepository NoteRepo { get; } = new();

    public static void SetConfiguration(IConfiguration config)
    {
        Configuration = config;
        _ConnectionString = config.GetConnectionString("DefaultConnection");
    }

    public static void SetConnectionString(string connectionString)
    {
        _ConnectionString = connectionString;
    }

    public static async Task<bool> CheckConnectionFor(string connectionString)
    {
        try
        {
            using var conn = new SqlConnection(connectionString);
            int value = await conn.QuerySingleAsync<int>("SELECT 12345", commandType: CommandType.Text);
            
            return value == 12345;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public static IDbConnection GetConnection
    {
        get
        {
            return new SqlConnection(_ConnectionString);
        }
    }
}
