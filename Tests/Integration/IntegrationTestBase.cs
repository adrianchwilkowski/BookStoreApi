using Infrastructure.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace Tests.Integration
{
    public class IntegrationTestBase
    {
        private static string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        protected string samplesDir = System.IO.Path.Combine(currentDir, @"..\..\..\Integration\Samples\"); 
        protected static ApplicationDbContext TestDbContext()
        {
            return new ApplicationDbContext(new DbContextOptionsBuilder()
                .UseSqlServer(test.ConnectionString)
                .Options);
        }

        private static SqlConnectionStringBuilder test =>
            new SqlConnectionStringBuilder
            {
                DataSource = @"(LocalDB)\MSSQLLocalDB",
                InitialCatalog = "test",
                IntegratedSecurity = true
            };
    }
}