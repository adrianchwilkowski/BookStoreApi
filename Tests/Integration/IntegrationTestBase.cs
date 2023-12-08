using Infrastructure.Migrations;
using Infrastructure;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data.SqlClient;
using Infrastructure.Repositories;

namespace Tests.Integration
{
    public class IntegrationTestBase
    {
        private static string currentDir = AppDomain.CurrentDomain.BaseDirectory;
        public static string samplesDir = System.IO.Path.Combine(currentDir, @"..\..\..\Integration\Samples\");
        public static ApplicationDbContext context = TestDbContext();

        protected readonly IBooksRepository booksRepository;

        public IntegrationTestBase() 
        {
            var services = new ServiceCollection();
            services.RegisterRepositories();
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(test.ConnectionString));
            var serviceProvider = services.BuildServiceProvider();
            booksRepository = serviceProvider.GetRequiredService<IBooksRepository>();
        }
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