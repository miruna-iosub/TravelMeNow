
using TravelMeNow.Application.Mapping;
using TravelMeNow.Application.Services;
using TravelMeNow.Application.Services.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TravelMeNow.Application;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(); 

        services.AddAutoMapper(typeof(PointOfInterestProfile));
        services.AddAutoMapper(typeof(OpeningHourProfile));

        services.AddScoped<IMapService, MapService>();
        services.AddScoped<IPointOfInterestService, PointOfInterestService>();
        services.AddScoped<IOpeningHourService, OpeningHourService>();

        return services;
    }
}
