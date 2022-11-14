using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;
using sakilaAppMySQL.Swagger.Film;
using Microsoft.AspNetCore.Authorization;

namespace sakilaAppMySQL.Controllers
{
  [Authorize]
  [ApiController]
  [Route("[controller]")]
  public class FilmController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IFilmService _service;
    private readonly ILogger<FilmController> _logger;

    public FilmController(ILogger<FilmController> logger, IMapper mapper, IFilmService service)
    {
      _mapper = mapper;
      _service = service;
      _logger = logger;
    }
    [HttpGet()]
    [SwaggerResponseExample(200, typeof(FilmsExample))]
    public IEnumerable<FilmDto> Get()
    {
      return _mapper.Map<IEnumerable<Film>, IEnumerable<FilmDto>>(_service.GetAll());
    }

    [HttpPost("searchByPage")]
    [SwaggerResponseExample(200, typeof(FilmsExample))]
    [SwaggerRequestExample(typeof(SearchFilmFilterDto), typeof(SearchFilterExample))]
    public IEnumerable<FilmDto> SearchByPage([FromBody] SearchFilmFilterDto filter)
    {
      return _mapper.Map<IEnumerable<Film>, IEnumerable<FilmDto>>(_service.SearchByPage(filter));
    }
   

    [HttpPost()]
    [SwaggerResponseExample(200, typeof(FilmExample))]
    [SwaggerRequestExample(typeof(CreateFilmDto), typeof(CreateFilmExample))]
    public FilmDto CreateFilm([FromBody] CreateFilmDto film)
    {
      return _mapper.Map<Film, FilmDto>(_service.CreateFilm(film));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <response code="404">Film not found</response>
    [HttpGet("{Id}")]
    [SwaggerResponseExample(200, typeof(FilmExample))]
    public FilmDto GetOne([FromRoute] int Id)
    {
      return _mapper.Map<Film, FilmDto>(_service.GetOne(Id));
    }


    /// <response code="404">Film not found</response>
    [HttpDelete()]
    public void Delte(int Id)
    {
      _service.Delete(Id);
    }
  }
}
