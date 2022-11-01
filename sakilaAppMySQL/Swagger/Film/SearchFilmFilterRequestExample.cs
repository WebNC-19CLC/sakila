using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Film
{
  public class SearchFilterExample : IMultipleExamplesProvider<SearchFilmFilterDto>
  {
    public IEnumerable<SwaggerExample<SearchFilmFilterDto>> GetExamples()
    {
      yield return SwaggerExample.Create(
        "200",
        new SearchFilmFilterDto
        {
          NumPerPage = 10,
          PageNum = 1,
        }
        );
    }
  }
}
