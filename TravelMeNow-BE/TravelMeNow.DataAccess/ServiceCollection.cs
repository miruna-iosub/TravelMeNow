using TravelMeNow.DataAccess.Persistence;
using TravelMeNow.DataAccess.Repositories;
using TravelMeNow.DataAccess.Repositories.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelMeNow.DataAccess;

public static class ServiceCollection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DatabaseContext>(options =>
        {
            options.UseNpgsql(configuration["Database:TravelMeNowString"]);
        });

        services.AddScoped<IPointOfInterestRepository, PointOfInterestRepository>();
        services.AddScoped<IOpeningHourRepository, OpeningHourRepository>();

        return services;
    }
}
