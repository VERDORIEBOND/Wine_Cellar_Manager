using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace Controller.Repositories;

public class CountryRepository
{
    /// <summary>
    /// Retrieves a CountryRecord based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A CountryRecord retrieved from database</returns>
    public async Task<CountryRecord> Get(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QuerySingleAsync<CountryRecord>(Queries.Country_Get, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Retrieves all CountryRecord entries in the database.
    /// </summary>
    /// <returns>List containing all CountryRecord entries.</returns>
    public async Task<IEnumerable<CountryRecord>> GetAll()
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<CountryRecord>(Queries.Country_GetAll, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Deletes a CountryRecord from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Delete(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Country_Delete, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Updates a CountryRecord from the database.
    /// </summary>
    /// <param name="country"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Update(CountryRecord country)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Country_Update, country, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Inserts a CountryRecord into the database.
    /// </summary>
    /// <param name="country"></param>
    /// <returns>The Id of the inserted record.</returns>
    public async Task<int> Create(CountryRecord country)
    {
        DynamicParameters parameters = new();
        parameters.Add("Name", country.Name);

        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.Country_Insert, parameters, commandType: CommandType.StoredProcedure);
    }
}
