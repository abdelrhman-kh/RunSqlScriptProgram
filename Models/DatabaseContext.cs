using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer.Management.Smo;
using SqlScript.Models;


namespace SqlScript.Models
    
{
    public class DatabaseContext : DbContext
    {
        public DbSet <Connections> DbConnections { get; set; }
        public DbSet<ConnectionString> DbConnectionString { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

        }
    }
}
