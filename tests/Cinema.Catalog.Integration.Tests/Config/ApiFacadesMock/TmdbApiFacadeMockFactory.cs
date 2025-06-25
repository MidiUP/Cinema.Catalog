using AutoFixture;
using Cinema.Catalog.Domain.Infrastructure.ApiFacades;
using Cinema.Catalog.Domain.Models;
using Moq;

namespace Cinema.Catalog.Integration.Tests.Config.ApiFacadesMock;

public static class TmdbApiFacadeMockFactory
{
    public static ITmdbApiFacade Build()
    {
        var _fixture = new Fixture();
        var mock = new Mock<ITmdbApiFacade>();

        mock.Setup(f => f.GetDetailsMovieAsync(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.Create<DetailsMovieModel>());
        
        mock.Setup(f => f.GetDetailsMovieAsync(999999, It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null!);

        mock.Setup(f => f.GetMoviesAsync(It.IsAny<SearchMoviesModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(_fixture.CreateMany<MovieModel>(10));

        mock.Setup(f => f.GetMoviesAsync(
            It.Is<SearchMoviesModel>(m => m.TermSearch == "FilmeInexistente" && m.PremiereYear == 1900),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(() => null!);


        return mock.Object;
    }
}
