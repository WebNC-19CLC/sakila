using sakilaAppMySQL.Dtos.ActorsDto;
using Swashbuckle.AspNetCore.Filters;

namespace sakilaAppMySQL.Swagger.Actor
{
  public class ActorsExample : IMultipleExamplesProvider<List<ActorDto>>
  {
    public IEnumerable<SwaggerExample<List<ActorDto>>> GetExamples()
    {
      yield return SwaggerExample.Create(
        "Example",
        new List<ActorDto>() {new ActorDto()
        {
          ActorId = 1,
          FirstName = "PENELOPE",
          LastName = "GUINESS",
          LastUpdate = DateTime.Now
        } }
      );
    }
  }
}
