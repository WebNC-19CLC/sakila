using sakilaAppMySQL.Dtos.FilmsDto;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public interface IFilmService
  {
    IEnumerable<Film> GetAll();
    Film CreateFilm(CreateFilmDto film);
    IEnumerable<Film> SearchByPage(SearchFilmFilterDto filter);
  }
}
