using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.ActorsDto;

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
    [HttpGet()]
    public IEnumerable<ActorDto> Get()
    {
      return _mapper.Map<IEnumerable<Actor>, IEnumerable<ActorDto>>(_service.GetAll());
    }

    [HttpPost()]
    public ActorDto CreateActor([FromBody] CreateActorDto actor) {
      return _service.Create(actor);
    }
  }
}
