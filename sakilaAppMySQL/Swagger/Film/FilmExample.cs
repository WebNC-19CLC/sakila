using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Film
{
  public class FilmExample : IMultipleExamplesProvider<FilmDto>
  {
    public IEnumerable<SwaggerExample<FilmDto>> GetExamples()
    {
      yield return SwaggerExample.Create("Example",
          new FilmDto
          {
            Id = 1,
            Title = "ACADEMY DINOSAUR",
            Description = "A Epic Drama of a Feminist And a Mad Scientist who must Battle a Teacher in The Canadian Rockies",
            ReleaseYear = 2006,
            RentalRate = 1,
            Length = 86,
            RentalDuration = 6,
            ReplacementCost = 21,
            Rating = "PG",
            LastUpdate = DateTime.Now,
            Language = "English",
            OriginalLanguage = null,
            FilmActors = new List<string>
          {
            "PENELOPE GUINESS",
            "CHRISTIAN GABLE",
            "LUCILLE TRACY",
            "SANDRA PECK",
            "JOHNNY CAGE",
            "MENA TEMPLE",
            "WARREN NOLTE",
            "OPRAH KILMER",
            "ROCK DUKAKIS",
            "MARY KEITEL"
          },
            FilmCategories = new List<string>
          {
             "Documentary"
          },
            Inventories = new List<string>()
          }
      );
    }
  }
}
