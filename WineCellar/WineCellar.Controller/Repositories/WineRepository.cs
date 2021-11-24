using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Model;

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
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Wine_Update, wine, commandType: CommandType.StoredProcedure);
    }

    public async Task<int> Create(WineRecord wine)
    {
        DynamicParameters parameters = new();
        parameters.Add("Name", wine.Name);
        parameters.Add("Buy", wine.Buy, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Sell", wine.Sell, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Type", wine.Type);
        parameters.Add("Country", wine.Country);
        parameters.Add("Picture", wine.Picture);
        parameters.Add("Year", wine.Year);
        parameters.Add("Content", wine.Content);
        parameters.Add("Alcohol", wine.Alcohol, dbType: DbType.Decimal, precision: 10, scale: 2);
        parameters.Add("Rating", wine.Rating);
        parameters.Add("Description", wine.Description);

        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.Wine_Insert, parameters, commandType: CommandType.StoredProcedure);
    }
}
