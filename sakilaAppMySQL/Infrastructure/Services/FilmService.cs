using AutoMapper;
using Microsoft.EntityFrameworkCore;
using sakilaAppMySQL.Dtos.FilmsDto;
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

    public IEnumerable<Film> SearchByPage(SearchFilmFilterDto filter)
    {
      return _context.Films.Include(x => x.FilmActors).ThenInclude(x => x.Actor)
       .Include(x => x.Language)
       .Include(x => x.OriginalLanguage)
       .Include(x => x.FilmCategories).ThenInclude(x => x.Category)
       .Include(x => x.Inventories).ThenInclude(x => x.Store).Skip(filter.NumPerPage * (filter.PageNum - 1)).Take(filter.NumPerPage).ToList();
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

      var actors = _context.Actors.Where(x => film.FilmActorIds.Contains(x.ActorId)).Select(x => new FilmActor { Film = filmToSave, Actor = x, LastUpdate = DateTimeNow }).ToList();
      var categories = _context.Categories.Where(x => film.FilmCategoriesIds.Contains(x.CategoryId)).Select(x => new FilmCategory { Film = filmToSave, Category = x, LastUpdate = DateTimeNow }).ToList();

      filmToSave.LastUpdate = DateTimeNow;
      filmToSave.Language = language;
      filmToSave.OriginalLanguage = originalLanguage;
      filmToSave.FilmActors = actors;
      filmToSave.FilmCategories = categories;

      _context.Add(filmToSave);
      _context.SaveChanges();

      return filmToSave;
    }

    public void Delete(int Id)
    {
      var film = _context.Films.SingleOrDefault(x => x.FilmId == Id);
      if (film == null)
      {
        throw new NotFoundException<Film>(Id);
      }
      _context.Remove(film);
      _context.SaveChanges();
    }

    public Film GetOne(int Id)
    {
      var film = _context.Films.Include(x => x.FilmActors).ThenInclude(x => x.Actor)
        .Include(x => x.Language)
        .Include(x => x.OriginalLanguage)
        .Include(x => x.FilmCategories).ThenInclude(x => x.Category)
        .Include(x => x.Inventories).ThenInclude(x => x.Store).SingleOrDefault(x => x.FilmId == Id);
      if(film == null)
      {
        throw new NotFoundException<Film>(Id);
      }

      return film;
    }
  }
}
