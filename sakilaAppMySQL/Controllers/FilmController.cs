using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Dtos.FilmsDto;

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
    public IEnumerable<FilmDto> Get()
    {
      return _mapper.Map<IEnumerable<Film>, IEnumerable<FilmDto>>(_service.GetAll());
    }

    [HttpPost("searchByPage")]
    public IEnumerable<FilmDto> SearchByPage([FromBody] SearchFilmFilterDto filter)
    {
      return _mapper.Map<IEnumerable<Film>, IEnumerable<FilmDto>>(_service.SearchByPage(filter));
    }

    [HttpPost()]
    public FilmDto CreateFilm([FromBody] CreateFilmDto film)
    {
      return _mapper.Map<Film, FilmDto>(_service.CreateFilm(film));
    }
  }
}
