using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Film
{
  public class CreateFilmExample : IMultipleExamplesProvider<CreateFilmDto>
  {
    public IEnumerable<SwaggerExample<CreateFilmDto>> GetExamples()
    {
      yield return SwaggerExample.Create("Example request",
        new CreateFilmDto
        {
          Title = "Type something",
          Description = "Type something",
          ReleaseYear = 2022,
          RentalRate = 22,
          Length = 2,
          RentalDuration = 6,
          ReplacementCost = 22,
          Rating = "PG",
          LanguageId = 1,
          OriginalLanguageId = 1,
          FilmActorIds = new List<int> { 1 },
          FilmCategoriesIds = new List<int> { 1 },
          Inventories = new List<int> { },
        }
       );
    }
  }
}
