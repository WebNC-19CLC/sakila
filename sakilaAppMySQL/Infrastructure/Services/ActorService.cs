using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Infrastructure.Context;
using sakilaAppMySQL.Dtos.ActorsDto;
using sakilaAppMySQL.Infrastructure.Exceptions;
using sakilaAppMySQL.Dtos.Common;

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

    public Actor GetOne(int Id) {
      var actor =  _context.Actors.SingleOrDefault(x => x.ActorId == Id);
      if (actor == null) {
        throw new NotFoundException<Actor>(Id);
      }
      return actor;
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

    public IEnumerable<Actor> SearchByPage(SearchFilterDto filter)
    {
      return _context.Actors.Skip(filter.NumPerPage * (filter.PageNum - 1)).Take(filter.NumPerPage).ToList();
    }

    public void Delete(int Id)
    {
      var actor = _context.Actors.SingleOrDefault(x => x.ActorId == Id);
      if (actor == null)
      {
        throw new NotFoundException<Actor>(Id);
      }
      _context.Remove(actor);
      _context.SaveChanges();
    }
  }
}
