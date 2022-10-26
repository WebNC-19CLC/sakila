using sakilaAppMySQL.Dtos;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public interface IFilmService
  {
    IEnumerable<Film> GetAll();
    Film CreateFilm(CreateFilmDto film);
  }
}
