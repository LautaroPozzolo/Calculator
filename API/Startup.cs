using API.Core.Extensions;
using API.Core.Middlewares;
using API.Repository.Implementations;
using API.Repository.Interfaces;
using API.Services.CalculatorServices;
using API.Services.Contracts;
using Application;
using AutoMapper;
using Domain;
using Domain.Models.Mapper;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace API;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();

        services.AddHealthChecks();
        services.ConfigureCors();
        services.ConfigureSwagger();

        services.ConfigureDomain();
        services.ConfigureApplication();
        services.ConfigureInfrastructure();
        services.ConfigureAPI();

        // Auto Mapper Configurations
        var mapperConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });

        IMapper mapper = mapperConfig.CreateMapper();
        services.AddSingleton(mapper);

        services.AddDbContext<CalculatorContext>(options =>
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), 
                b => b.MigrationsAssembly("API"));
        });

        services.AddScoped<ICalculatorRepository, CalculatorRepository>();
        services.AddScoped<ICalculatorService, CalculatorService>();

    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseNativeGlobalExceptionHandler();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseCors("CorsPolicy");

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Events API");
            c.EnableFilter();
            c.DisplayOperationId();
            c.DisplayRequestDuration();
        });

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHealthChecks("/health");
            endpoints.MapControllers();
        });

    }
}
