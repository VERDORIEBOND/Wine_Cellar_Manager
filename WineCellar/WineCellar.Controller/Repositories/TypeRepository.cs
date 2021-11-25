using Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Controller.Repositories;

public class TypeRepository
{
    /// <summary>
    /// Retrieves a TypeRecord based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A TypeRecord retrieved from database</returns>
    public async Task<TypeRecord> Get(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QuerySingleAsync<TypeRecord>(Queries.Type_Get, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Retrieves all TypeRecord entries in the database.
    /// </summary>
    /// <returns>List containing all TypeRecord entries.</returns>
    public async Task<IEnumerable<TypeRecord>> GetAll()
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<TypeRecord>(Queries.Type_GetAll, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Deletes a TypeRecord from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Delete(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Type_Delete, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Updates a TypeRecord from the database.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Update(TypeRecord type)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Type_Update, type, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Inserts a TypeRecord into the database.
    /// </summary>
    /// <param name="type"></param>
    /// <returns>Id of the inserted record.</returns>
    public async Task<int> Create(TypeRecord type)
    {
        DynamicParameters parameters = new();
        parameters.Add("Name", type.Name);

        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.Type_Insert, parameters, commandType: CommandType.StoredProcedure);
    }
}
