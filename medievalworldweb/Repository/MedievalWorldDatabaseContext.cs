using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace medievalworldweb.Repository
{
    public class MedievalWorldDatabaseContext : DbContext
    {
        public MedievalWorldDatabaseContext()
        {

        }
        public MedievalWorldDatabaseContext(DbContextOptions<MedievalWorldDatabaseContext> options) : base(options)
        {
        }

        public async Task ExecuteSqlAsync(string sql)
        {
            await Database.ExecuteSqlRawAsync(sql);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlServer(@"Data Source =.; Initial Catalog = MedievalWorld; Integrated Security = True; MultipleActiveResultSets = True; App = EntityFramework");
        }
        public async Task<int> SaveChangesAsync()
        {
            return await SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
