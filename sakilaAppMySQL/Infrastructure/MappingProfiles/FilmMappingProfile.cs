using sakilaAppMySQL.Dtos.FilmsDto;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.MappingProfiles
{
  public class FilmMappingProfile : BaseMappingProfile<Film>
  {
    public FilmMappingProfile()
    {
      CreateMap<Film, FilmDto>()
        .ForMember(des => des.Id, opt => opt.MapFrom(src => src.FilmId))
        .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title))
        .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
        .ForMember(des => des.RentalRate, opt => opt.MapFrom(src => src.RentalRate))
        .ForMember(des => des.RentalDuration, opt => opt.MapFrom(src => src.RentalDuration))
        .ForMember(des => des.ReplacementCost, opt => opt.MapFrom(src => src.ReplacementCost))
        .ForMember(des => des.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate))
        .ForMember(des => des.FilmCategories, opt => opt.MapFrom(src => src.FilmCategories.Select(x => x.Category.Name).ToList()))
        .ForMember(des => des.FilmActors, opt => opt.MapFrom(src => src.FilmActors.Select(x => GetFullName(x.Actor.FirstName, x.Actor.LastName))))
        .ForMember(des => des.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
        .ForMember(des => des.Length, opt => opt.MapFrom(src => src.Length))
        .ForMember(des => des.OriginalLanguage, opt => opt.MapFrom(src => src.OriginalLanguage.Name))
        .ForMember(des => des.Language, opt => opt.MapFrom(src => src.Language.Name))
        .ForMember(des => des.Rating, opt => opt.MapFrom(src => src.Rating))
        .ForMember(des => des.Inventories, opt => opt.MapFrom(src => src.Inventories.Select(x => x.Store.Address).ToList()));

      CreateMap<CreateFilmDto, Film>()
       .ForMember(des => des.FilmId, opt => short.Parse("0"))
       .ForMember(des => des.Title, opt => opt.MapFrom(src => src.Title))
       .ForMember(des => des.Description, opt => opt.MapFrom(src => src.Description))
       .ForMember(des => des.RentalRate, opt => opt.MapFrom(src => src.RentalRate))
       .ForMember(des => des.ReleaseYear, opt => opt.MapFrom(src => src.ReleaseYear))
       .ForMember(des => des.Length, opt => opt.MapFrom(src => src.Length))
       .ForMember(des => des.RentalDuration, opt => opt.MapFrom(src => src.RentalDuration))
       .ForMember(des => des.Rating, opt => opt.MapFrom(src => src.Rating))
       .ForMember(des => des.ReplacementCost, opt => opt.MapFrom(src => src.ReplacementCost))
       .ForMember(des => des.Inventories, opt => opt.Ignore());
    }

    private static string GetFullName(string firstname, string lastname)
    {
      return firstname + " " + lastname;
    }
  }
}
