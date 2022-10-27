using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Infrastructure.Context;
using sakilaAppMySQL.Dtos.ActorsDto;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public class ActorService : IActorService
  {
    private readonly sakilaContext _context;
    public ActorService(sakilaContext context)
    {
      _context = context;
    }
    public IEnumerable<Actor> GetAll()
    {
      return _context.Actors.ToList();
    }

    public ActorDto Create(CreateActorDto actor)
    {
      var actorToAdd = new Actor { ActorId = 0, LastUpdate = DateTime.UtcNow, FirstName = actor.FirstName, LastName = actor.LastName };
      _context.Actors.Add(actorToAdd);
      _context.SaveChanges();
      return new ActorDto
      {
        ActorId = actorToAdd.ActorId,
        FirstName = actorToAdd.FirstName,
        LastName = actorToAdd.LastName,
        LastUpdate = actorToAdd.LastUpdate
      };
    }
  }
}
