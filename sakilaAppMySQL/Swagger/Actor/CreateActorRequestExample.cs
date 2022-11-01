using sakilaAppMySQL.Dtos.ActorsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Actor
{
  public class CreateActorRequestExample : IMultipleExamplesProvider<CreateActorDto>
  {
    public IEnumerable<SwaggerExample<CreateActorDto>> GetExamples()
    {
      yield return SwaggerExample.Create("Request example",
        new CreateActorDto
        {
          FirstName = "Mai",
          LastName = "Anh"
        });
    }
  }
}
