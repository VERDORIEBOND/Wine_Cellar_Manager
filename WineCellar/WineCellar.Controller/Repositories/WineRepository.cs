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
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.Wine_Insert, wine, commandType: CommandType.StoredProcedure);
    }
}
