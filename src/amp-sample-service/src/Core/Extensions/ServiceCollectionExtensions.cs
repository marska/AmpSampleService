using AmpSampleService.APIs;

namespace AmpSampleService;

public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Add services to the container.
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IAisService, AisService>();
    }
}
