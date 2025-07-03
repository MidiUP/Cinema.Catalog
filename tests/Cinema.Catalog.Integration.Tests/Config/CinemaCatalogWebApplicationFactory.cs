using Cinema.Catalog.API;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Integration.Tests.Config.ApiFacadesMock;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cinema.Catalog.Integration.Tests.Config;

public class CinemaCatalogWebApplicationFactory : WebApplicationFactory<Program>
{
    protected IServiceScope? Scope { get; private set; }
    protected HttpClient? Client { get; private set; }

    public CinemaCatalogWebApplicationFactory()
    {
        Client = CreateClient();    
        Scope = Services.CreateScope();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((context, configBuilder) =>
        {
            configBuilder
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
        });

        builder.ConfigureServices(ConfigureServices);

        base.ConfigureWebHost(builder);
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        // Remove a implementação real
        services.RemoveAll<ITmdbApiFacade>();

        // Adiciona o mock
        var tmdbApiFacadeMock = TmdbApiFacadeMockFactory.Build();
        services.AddSingleton<ITmdbApiFacade>(tmdbApiFacadeMock);

    }

    [OneTimeTearDown]
    protected void ClearAfterAllTests()
    {
        base.Dispose();
    }
}
