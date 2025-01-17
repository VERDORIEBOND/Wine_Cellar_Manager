﻿using WineCellar.Model;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Controller.Repositories;

public class StorageLocationRepository
{
    /// <summary>
    /// Retrieves all StorageLocationRecord entries in the database.
    /// </summary>
    /// <returns>List containing all StorageLocationRecord entries.</returns>
    public async Task<IEnumerable<StorageLocation>> GetAll()
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<StorageLocation>(Queries.StorageLocation_GetAll, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Retrieves all StorageLocationRecord entries in the database based on WineId.
    /// </summary>
    /// <param name="wineId"></param>
    /// <returns>List containing all StorageLocationRecord entries based on WineId</returns>
    public async Task<IEnumerable<StorageLocation>> GetByWine(int wineId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<StorageLocation>(Queries.StorageLocation_GetByWine, new { IdWine = wineId }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Deletes a StorageLocationRecord from the database.
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Delete(StorageLocation location)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.StorageLocation_Delete, location, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Deletes all StorageLocationRecords associated with provided WineId from the database.
    /// </summary>
    /// <param name="wineId"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> DeleteAllByWine(int wineId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.StorageLocation_DeleteAllByWine, new { IdWine = wineId }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Inserts a StorageLocationRecord into the database.
    /// </summary>
    /// <param name="location"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Create(StorageLocation location)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.StorageLocation_Insert, location, commandType: CommandType.StoredProcedure);
    }
}
