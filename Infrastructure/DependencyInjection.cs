namespace Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Migrations;
using Infrastructure.Repositories;

public static class DependencyInjection
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
    }
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBooksRepository, BooksRepository>();
        services.AddTransient<IBookInfoRepository, BookInfoRepository>();
    }
}