using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.FilmsDto;
using Swashbuckle.AspNetCore.Filters;
using sakilaAppMySQL.Swagger.Film;

namespace sakilaAppMySQL.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class FilmController : ControllerBase
  {
    private readonly IMapper _mapper;
    private readonly IFilmService _service;
    public FilmController(IMapper mapper, IFilmService service)
    {
      _mapper = mapper;
      _service = service;
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
