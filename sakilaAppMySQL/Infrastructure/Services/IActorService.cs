using sakilaAppMySQL.Dtos;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public interface IActorService
  {
    IEnumerable<Actor> GetAll();
    ActorDto Create(CreateActorDto actor);

  }
}
