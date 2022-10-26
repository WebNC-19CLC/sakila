using sakilaAppMySQL.Dtos;
using sakilaAppMySQL.Infrastructure.Domain.Entities;

namespace sakilaAppMySQL.Infrastructure.MappingProfiles
{
  public class ActorMappingProfile : BaseMappingProfile<Actor>
  {
    public ActorMappingProfile() {
      CreateMap<Actor, ActorDto>()
       .ForMember(des => des.ActorId, opt => opt.MapFrom(src => src.ActorId))
       .ForMember(des => des.FirstName, opt => opt.MapFrom(src => src.FirstName))
       .ForMember(des => des.LastName, opt => opt.MapFrom(src => src.LastName))
       .ForMember(des => des.LastUpdate, opt => opt.MapFrom(src => src.LastUpdate));
    }
  }
}
