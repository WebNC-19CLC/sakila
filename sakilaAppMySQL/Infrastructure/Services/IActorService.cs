using sakilaAppMySQL.Dtos.ActorsDto;
using sakilaAppMySQL.Dtos.Common;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public interface IActorService
  {
    IEnumerable<Actor> GetAll();
    ActorDto Create(CreateActorDto actor);
    Actor GetOne(int Id);
    IEnumerable<Actor> SearchByPage(SearchFilterDto filter);
    void Delete(int Id);
  }
}
