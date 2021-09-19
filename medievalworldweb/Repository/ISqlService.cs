using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public interface ISqlService
    {
        Task ExecuteSql(string sql);
    }

    public class SqlService : GenericRepository<MedievalWorldDatabaseContext>, ISqlService
    {
        public async Task ExecuteSql(string sql)
        {
            await ExecuteSqlAsync(sql);
        }
    }
}
