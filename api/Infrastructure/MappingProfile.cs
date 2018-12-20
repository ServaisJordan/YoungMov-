using model;
using DTO;

namespace api.Infrastructure {
    public class MappingProfile : AutoMapper.Profile {
        public MappingProfile() {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Carpooling, CarpoolingDTO>().ReverseMap();
            CreateMap<CarpoolingApplicant, CarpoolingApplicantDTO>().ReverseMap();
            CreateMap<PrivateMessage, PrivateMessageDTO>().ReverseMap();
        }
    }
}