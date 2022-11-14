using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.ActorsDto;
using Swashbuckle.AspNetCore.Filters;
using sakilaAppMySQL.Swagger.Actor;
using sakilaAppMySQL.Dtos.Common;
using sakilaAppMySQL.Swagger.Common;
using Microsoft.AspNetCore.Authorization;

namespace sakilaAppMySQL.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class ActorController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IActorService _service;
    private readonly ILogger<ActorController> _logger;

    public ActorController(ILogger<ActorController> logger, IMapper mapper, IActorService service)
    {
      _mapper = mapper;
      _service = service;
      _logger = logger;
    }
    /// <summary>
    /// Get all actors
    /// </summary>
    /// <returns>A list of Actors</returns>
    /// <response code="400">Bad request</response>
    [HttpGet()]
    [SwaggerResponseExample(200, typeof(ActorsExample))]
    public IEnumerable<ActorDto> Get()
    {
      return _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorDto>>(_service.GetAll());
    }

    /// <summary>
    /// Create an actor
    /// </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    [HttpPost()]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    [SwaggerRequestExample(typeof(CreateActorDto), typeof(CreateActorRequestExample))]
    public ActorDto CreateActor([FromBody] CreateActorDto actor)
    {
      return _service.Create(actor);
    }

    /// <summary>
    /// Get one actor
    /// </summary>
    /// <param name="Id">Id of an actor</param>
    /// <response code="404">Actor not found</response>
    [HttpGet("{Id}")]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    public ActorDto GetOne([FromRoute] int Id)
    {
      return _mapper.Map<Actor, ActorDto>(_service.GetOne(Id));
    }

    /// <summary>
    /// Get actor by page
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("searchByPage")]
    [SwaggerResponseExample(200, typeof(ActorExample))]
    [SwaggerRequestExample(typeof(SearchFilterDto), typeof(SearchFilterExample))]
    public IEnumerable<ActorDto> SearchByPage([FromBody] SearchFilterDto filter)
    {
      return _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorDto>>(_service.SearchByPage(filter));
    }

    /// <summary>
    /// Delete an actor
    /// </summary>
    /// <param name="Id">Id of an actor</param>
    /// <response code="404">Actor not found</response>
    [HttpDelete()]
    public void Delte(int Id)
    {
      _service.Delete(Id);
    }
  }
}
