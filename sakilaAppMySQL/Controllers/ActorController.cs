using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.ActorsDto;
using Swashbuckle.AspNetCore.Filters;
using sakilaAppMySQL.Swagger.Actor;

namespace sakilaAppMySQL.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ActorController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IActorService _service;
    public ActorController(IMapper mapper, IActorService service)
    {
      _mapper = mapper;
      _service = service;
    }
    /// <summary>
    /// Get all actors
    /// </summary>
    /// <returns>A list of Actors</returns>
    /// <response code="201">Returns the newly created item</response>
    [HttpGet()]
    [SwaggerResponseExample(200, typeof(ActorsExample))]
    public IEnumerable<ActorDto> Get()
    {
      return _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorDto>>(_service.GetAll());
    }

    [HttpPost()]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    [SwaggerRequestExample(typeof(CreateActorDto), typeof(CreateActorRequestExample))]
    public ActorDto CreateActor([FromBody] CreateActorDto actor) {
      return _service.Create(actor);
    }
  }
}
