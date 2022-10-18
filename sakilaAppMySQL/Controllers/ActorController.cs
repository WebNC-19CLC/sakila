using Microsoft.AspNetCore.Mvc;
using sakilaAppMySQL.Dtos;
using sakilaAppMySQL.Infrastructure.Services;

namespace sakilaAppMySQL.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ActorController : ControllerBase
  {
    private readonly ILogger<ActorController> _logger;
    private readonly ActorService _service;
    public ActorController(ILogger<ActorController> logger, ActorService service)
    {
      _logger = logger;
      _service = service;
    }
    [HttpGet()]
    public IEnumerable<ActorDto> Get()
    {
      return _service.GetAll();
    }

    [HttpPost()]
    public ActorDto CreateActor([FromBody] CreateActorDto actor) {
      return _service.Create(actor);
    }
  }
}
