using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WineCellar.Model;
using Dapper;
using System.Data;

namespace Controller.Repositories;

public class NoteRepository
{

    /// <summary>
    /// Retrieves a NoteRecord based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A NoteRecord retrieved from database</returns>
    public async Task<NoteRecord> Get(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QuerySingleAsync<NoteRecord>(Queries.Note_Get, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Retrieves all NoteRecord entries in the database.
    /// </summary>
    /// <returns>List containing all NoteRecord entries.</returns>
    public async Task<IEnumerable<NoteRecord>> GetAll()
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<NoteRecord>(Queries.Note_GetAll, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Retrieves a NoteRecord based on Id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>List containing all NoteRecord retrieved from database based on provided id</returns>
    public async Task<IEnumerable<NoteRecord>> GetByWine(int wineId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.QueryAsync<NoteRecord>(Queries.Note_GetByWine, new { Id = wineId }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Deletes a NoteRecord from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Delete(int id)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Note_Delete, new { Id = id }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Updates a NoteRecord from the database.
    /// </summary>
    /// <param name="note"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> Update(NoteRecord note)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Note_Update, note, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Inserts a NoteRecord into the database.
    /// </summary>
    /// <param name="note"></param>
    /// <returns>Id of the inserted record.</returns>
    public async Task<int> Create(NoteRecord note)
    {
        DynamicParameters parameters = new();
        parameters.Add("Name", note.Name);

        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteScalarAsync<int>(Queries.Note_Insert, parameters, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Inserts the id of wine and note into the database, these entries should exist before.
    /// </summary>
    /// <param name="wineId"></param>
    /// <param name="noteId"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> AddWine(int wineId, int noteId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Note_AddWine, new { IdWine = wineId, IdNote = noteId }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Removes the association between wine and note, these entries should exist before.
    /// </summary>
    /// <param name="wineId"></param>
    /// <param name="noteId"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> RemoveWine(int wineId, int noteId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Note_RemoveWine, new { IdWine = wineId, IdNote = noteId }, commandType: CommandType.StoredProcedure);
    }

    /// <summary>
    /// Removes all association with the provided wineid.
    /// </summary>
    /// <param name="wineId"></param>
    /// <returns>Rows affected</returns>
    public async Task<int> RemoveByWineId(int wineId)
    {
        using var conn = DataAccess.GetConnection;
        return await conn.ExecuteAsync(Queries.Note_RemoveByWineId, new { IdWine = wineId}, commandType: CommandType.StoredProcedure);
    }
}
