using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.ActorsDto;
using Swashbuckle.AspNetCore.Filters;
using sakilaAppMySQL.Swagger.Actor;
using sakilaAppMySQL.Dtos.Common;
using sakilaAppMySQL.Swagger.Common;

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
    /// <response code="400">Error</response>
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

    /// <summary>
    /// 
    /// </summary>
    /// <response code="404">Actor not found</response>
    [HttpGet("{Id}")]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    public ActorDto GetOne([FromRoute] int Id)
    {
      return _mapper.Map<Actor, ActorDto>(_service.GetOne(Id));
    }

    [HttpPost("searchByPage")]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    [SwaggerRequestExample(typeof(SearchFilterDto), typeof(SearchFilterExample))]
    public IEnumerable<ActorDto> SearchByPage([FromBody] SearchFilterDto filter)
    {
      return _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorDto>>(_service.SearchByPage(filter));
    }

    /// <response code="404">Actor not found</response>
    [HttpDelete()]
    public void Delte(int Id) { 
      _service.Delete(Id);
    }
  }
}
