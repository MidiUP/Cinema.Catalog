namespace Cinema.Catalog.Domain.Models
{
	public class Genre
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
    }

	public class ProductionCompany
	{
		public int Id { get; set; }
		public string LogoPath { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string OriginCountry { get; set; } = null!;
    }

	public class ProductionCountry
	{
		public string Iso3166_1 { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

	public class SpokenLanguage
	{
		public string EnglishName { get; set; } = null!;
        public string Iso639_1 { get; set; } = null!;
        public string Name { get; set; } = null!;
    }

	public class DetailsMovieModel
	{
		public bool Adult { get; set; }
		public string BackdropPath { get; set; } = null!;
        public object BelongsToCollection { get; set; } = null!;
        public int Budget { get; set; }
		public List<Genre> Genres { get; set; } = null!;
        public string Homepage { get; set; } = null!;
        public int Id { get; set; }
		public string ImdbId { get; set; } = null!;
        public List<string> OriginCountry { get; set; } = null!;
        public string OriginalLanguage { get; set; } = null!;
        public string OriginalTitle { get; set; } = null!;
        public string Overview { get; set; } = null!;
        public double Popularity { get; set; }
        public string PosterPath { get; set; } = null!;
        public List<ProductionCompany> ProductionCompanies { get; set; } = null!;
        public List<ProductionCountry> ProductionCountries { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public long Revenue { get; set; }
		public int Runtime { get; set; }
		public List<SpokenLanguage> SpokenLanguages { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Tagline { get; set; } = null!;
        public string Title { get; set; } = null!;
        public bool Video { get; set; }
		public double VoteAverage { get; set; }
		public int VoteCount { get; set; }
	}

}
