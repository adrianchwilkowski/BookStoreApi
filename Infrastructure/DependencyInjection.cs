namespace Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Infrastructure.Migrations;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public static class DependencyInjection
{
    public static void RegisterDbContext(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

        services.AddIdentityCore<IdentityUser>(options =>
        {
            options.ClaimsIdentity.RoleClaimType = "role";
            options.ClaimsIdentity.UserNameClaimType = "name";
        }).AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
    public static void RegisterRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBooksRepository, BooksRepository>();
        services.AddTransient<IBookInfoRepository, BookInfoRepository>();
    }
}