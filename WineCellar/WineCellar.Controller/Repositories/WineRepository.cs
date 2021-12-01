using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using WineCellar.Model;

namespace Controller.Repositories;

public class WineRepository
{
    public async Task<WineRecord> Get(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QuerySingleAsync<WineRecord>(Queries.Wine_Get, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task<IEnumerable<WineRecord>> GetAll()
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<WineRecord>(Queries.Wine_GetAll, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> Delete(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Wine_Delete, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> Update(WineRecord wine)
    {
        DynamicParameters parameters = new();
        parameters.Add("Id", wine.Id);
        parameters.Add("Name", wine.Name);
        parameters.Add("Buy", wine.Buy, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Sell", wine.Sell, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("TypeId", wine.TypeId);
        parameters.Add("CountryId", wine.CountryId);
        parameters.Add("Picture", wine.Picture);
        parameters.Add("Year", wine.Year);
        parameters.Add("Content", wine.Content);
        parameters.Add("Alcohol", wine.Alcohol, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Rating", wine.Rating);
        parameters.Add("Description", wine.Description);

        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Wine_Update, parameters, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> Create(WineData wine)
    {
        DynamicParameters parameters = new();
        parameters.Add("Name", wine.Name);
        parameters.Add("Buy", wine.BuyPrice, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Sell", wine.SellPrice, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("CountryId", 1);
        parameters.Add("TypeId", 1);
        parameters.Add("Picture", wine.Picture);
        parameters.Add("Year", wine.Age);
        parameters.Add("Content", wine.Contents);
        parameters.Add("Alcohol", wine.Alcohol, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Rating", wine.Rating);
        parameters.Add("Description", wine.Description);
        using var conn = DataAccess.GetConnection;
        string sql = "INSERT INTO Wine (Name, Buy, Sell, TypeId, CountryId, Picture, Year," +
            "Content, Alcohol, Rating, Description) VALUES (@Name, @Buy, @Sell, @TypeId, @CountryId," +
            "@Picture, @Year, @Content, @Alcohol, @Rating, @Description)";
        return await conn.ExecuteAsync(sql, parameters);
    }
}
