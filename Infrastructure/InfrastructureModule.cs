using Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class InfrastructureModule
{
    public static void ConfigureInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<CalculatorContext>();
    }
}
