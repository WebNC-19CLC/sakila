using sakilaAppMySQL.Dtos.Common;
using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Common
{
  public class SearchFilterExample : IMultipleExamplesProvider<SearchFilterDto>
  {
    public IEnumerable<SwaggerExample<SearchFilterDto>> GetExamples()
    {
      yield return SwaggerExample.Create(
        "Example request",
        new SearchFilterDto
        {
          NumPerPage = 10,
          PageNum = 1,
        }
        );
    }
  }
}
