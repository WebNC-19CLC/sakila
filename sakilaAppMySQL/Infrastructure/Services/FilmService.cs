using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sakilaAppMySQL.Dtos;
using sakilaAppMySQL.Infrastructure.Context;
using sakilaAppMySQL.Infrastructure.Domain.Entities;
using sakilaAppMySQL.Infrastructure.Exceptions;

namespace sakilaAppMySQL.Infrastructure.Services
{
  public class FilmService : IFilmService
  {
    private readonly sakilaContext _context;
    private IMapper _mapper;
    public FilmService(sakilaContext context, IMapper mapper)
    {
      _context = context;
      _mapper = mapper;
    }
    public IEnumerable<Film> GetAll()
    {
      return _context.Films.Include(x => x.FilmActors).ThenInclude(x => x.Actor)
        .Include(x => x.Language)
        .Include(x => x.OriginalLanguage)
        .Include(x => x.FilmCategories).ThenInclude(x => x.Category)
        .Include(x => x.Inventories).ThenInclude(x => x.Store).ToList();
    }

    public Film CreateFilm(CreateFilmDto film)
    {

      var filmToSave = _mapper.Map<CreateFilmDto, Film>(film);

      var DateTimeNow = DateTime.UtcNow;

      var originalLanguage = _context.Languages.SingleOrDefault(x => x.LanguageId == film.OriginalLanguageId);

      var language = _context.Languages.SingleOrDefault(x => x.LanguageId == film.LanguageId);
      if (language == null) {
        throw new NotFoundException<Language>(film.LanguageId);
      }

      var actors = _context.Actors.Where(x => film.FilmActorIds.Contains(x.ActorId)).ToList();
      var categories = _context.Categories.Where(x => film.FilmCategoriesIds.Contains(x.CategoryId)).ToList();

      var filmactors = new List<FilmActor>();
      foreach (var actor in actors) {
        filmactors.Add(new FilmActor { Film = filmToSave, Actor = actor, LastUpdate = DateTimeNow });
      }

      var filmCategories = new List<FilmCategory>();
      foreach (var category in categories)
      {
        filmCategories.Add(new FilmCategory { Film = filmToSave, Category = category, LastUpdate = DateTimeNow });
      }

      filmToSave.LastUpdate = DateTimeNow;
      filmToSave.Language = language;
      filmToSave.OriginalLanguage = originalLanguage;
      filmToSave.FilmActors = filmactors;
      filmToSave.FilmCategories = filmCategories;

      _context.Add(filmToSave);
      _context.SaveChanges();

      return filmToSave;
    }
  }
}
