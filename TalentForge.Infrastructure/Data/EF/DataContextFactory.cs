using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Telegramper.Core.Helpers.Factories.Configuration;

namespace TalentForge.Infrastructure.Data.EF;

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    private const string ConnectionStringName = "LocalConnection";

    public DataContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder<DataContext>();
        ConfigureOptions(options);
        return new DataContext(options.Options);
    }

    public static void ConfigureOptions(DbContextOptionsBuilder optionsBuilder)
    {
        const string connectionString = "Server=DESKTOP-814FASA;Database=TalentForgeLocalDB;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True"; //configuration.GetConnectionString(ConnectionStringName);
        optionsBuilder.UseSqlServer(connectionString);
    }
}