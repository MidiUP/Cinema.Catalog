using Cinema.Catalog.API;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Integration.Tests.Config.ApiFacadesMock;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
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
        ConfigureEnvironmentVariables();
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

    public static void ConfigureEnvironmentVariables()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Environment.SetEnvironmentVariable("ENV", "test");
        Environment.SetEnvironmentVariable("TMDB_API_BASE_URL", "https://api.themoviedb.org/3/");
        Environment.SetEnvironmentVariable("TMDB_API_KEY", "1f54bd990f1cdfb230adb312546d765d");
        Environment.SetEnvironmentVariable("TMDB_API_AUTH_TOKEN", "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiI0ZWQ5Y2E0NTUzZThiZmRmMjk5NjI1ZDI4ZjNlMGM0NCIsIm5iZiI6MTcyODQxODM3OS4zNzk5MjIsInN1YiI6IjY3MDU4Yjc1MDAwMDAwMDAwMDU4NTNiMiIsInNjb3BlcyI6WyJhcGlfcmVhZCJdLCJ2ZXJzaW9uIjoxfQ.p-MmF0K7-ku9kDlcyg4Ry8IeQMiufz5zTK-VT5wuOu8");
    }

    [OneTimeTearDown]
    protected void ClearAfterAllTests()
    {
        base.Dispose();
    }
}
