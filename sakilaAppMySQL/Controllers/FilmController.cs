using Microsoft.AspNetCore.Mvc;
using sakilaAppMySQL.Dtos;
using AutoMapper;
using sakilaAppMySQL.Infrastructure.Services;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

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

    [HttpPost()]
    public FilmDto CreateFilm([FromBody] CreateFilmDto film)
    {
      return _mapper.Map<Film, FilmDto>(_service.CreateFilm(film));
    }
  }
}
