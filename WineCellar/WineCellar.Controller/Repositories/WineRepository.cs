using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Controller.Repositories;

public record WineRecord(int Id, string Name, decimal Buy, decimal Sell, int Type, int Country, string Picture, int Year, int Content, decimal Alcohol, int Rating, string Description);

public class WineRepository
{
    public async Task<WineRecord> Get(int id)
    {
        using (var conn = DataAccess.GetConnection)
        {
            return await conn.QuerySingleAsync<WineRecord>("SELECT [Id],[Name],[Buy],[Sell],[Type],[Country],[Picture],[Year],[Content],[Alcohol],[Rating],[Description] FROM [dbo].[Wine] WHERE Id=@Id", new { Id = id });
        }
    }
}
