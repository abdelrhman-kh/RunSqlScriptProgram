using Microsoft.EntityFrameworkCore;
using SqlScript.Models;


namespace SqlScript.Models
    
{
    public class DatabaseContext : DbContext
    {
        public DbSet <Connections> DbConnections { get; set; }
        public DbSet<ConnectionString> DbConnectionString { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ManualScriptDB;User Id=test;Password=test;encrypt=false;");
            
        }
    }
}
