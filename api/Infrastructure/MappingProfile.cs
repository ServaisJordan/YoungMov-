using model;
using System.Linq;
using AutoMapper;

namespace api.Infrastructure {
    public class MappingProfile : AutoMapper.Profile {
        public MappingProfile() {
            CreateMap<User, DTO.UserControllerDTO.UserDTO>();
            CreateMap<DTO.UserControllerDTO.UserDTO, User>().ForMember(u => u.Carpooling, opt => opt.Ignore())
                                                            .ForMember(u => u.Car, opt => opt.Ignore())
                                                            .ForMember(u => u.PrivateMessage, opt => opt.Ignore())
                                                            .ForMember(u => u.CarpoolingApplicant, opt => opt.Ignore())
                                                            .ForMember(u => u.CreatedAt, opt => opt.Ignore())
                                                            .ForMember(u => u.UpdatedAt, opt => opt.Ignore())
                                                            .ForMember(u => u.TrustedCarpoolingDriverUserNavigation, opt => opt.Ignore());
            CreateMap<User, DTO.UserControllerDTO.UserDTORegistration>().ReverseMap();
            CreateMap<Car, DTO.UserControllerDTO.CarDTO>().ReverseMap();
            CreateMap<Carpooling, DTO.UserControllerDTO.CarpoolingDTO>().ReverseMap();
            CreateMap<PrivateMessage, DTO.UserControllerDTO.PrivateMessageDTO>().ReverseMap();
            CreateMap<CarpoolingApplicant, DTO.UserControllerDTO.CarpoolingApplicantDTO>().ReverseMap();
            CreateMap<TrustedCarpoolingDriver, DTO.UserControllerDTO.TrustedCarpoolingDriverDTO>().ReverseMap();

            CreateMap<User, DTO.CarpoolingControllerDTO.UserDTO>().ReverseMap();
            CreateMap<Car, DTO.CarpoolingControllerDTO.CarDTO>().ReverseMap();
            CreateMap<Carpooling, DTO.CarpoolingControllerDTO.CarpoolingDTO>();
            CreateMap<DTO.CarpoolingControllerDTO.CarpoolingDTO, Carpooling>().ForMember(c => c.CarpoolingApplicant, opt => opt.Ignore())
                                                                            .ForMember(c => c.CarNavigation, opt => opt.Ignore())
                                                                            .ForMember(c => c.CreatedAt, opt => opt.Ignore())
                                                                            .ForMember(c => c.UpdatedAt, opt => opt.Ignore())
                                                                            .ForMember(c => c.CreatorNavigation, opt => opt.Ignore());

            CreateMap<User, DTO.CarControllerDTO.UserDTO>().ReverseMap();
            CreateMap<Car, DTO.CarControllerDTO.UserDTO>();
            CreateMap<DTO.CarControllerDTO.CarDTO, Car>().ForMember(c => c.CreatedAt, opt => opt.Ignore())
                                                        .ForMember(c => c.OwnerNavigation, opt => opt.Ignore())
                                                        .ForMember(c => c.Carpooling, opt => opt.Ignore());

            CreateMap<User, DTO.PrivateMessageControllerDTO.UserDTO>().ReverseMap();
            CreateMap<PrivateMessage, DTO.PrivateMessageControllerDTO.PrivateMessageDTO>();
            CreateMap<DTO.PrivateMessageControllerDTO.PrivateMessageDTO, PrivateMessage>().ForMember(p => p.CreatedAt, opt => opt.Ignore())
                                                                                        .ForMember(p => p.CreatorNavigation, opt => opt.Ignore())
                                                                                        .ForMember(p => p.ReponseNavigation, opt => opt.Ignore());

            CreateMap<TrustedCarpoolingDriver, DTO.TrustedCarpoolingDriverControllerDTO.TrustedCarpoolingDriverDTO>();
            CreateMap<DTO.TrustedCarpoolingDriverControllerDTO.TrustedCarpoolingDriverDTO, TrustedCarpoolingDriver>().ForMember(t => t.CarpoolerNavigation, opt => opt.Ignore())
                                                                                                                    .ForMember(t => t.UserNavigation, opt => opt.Ignore())
                                                                                                                    .ForMember(t => t.CreatedAt, opt => opt.Ignore());

            CreateMap<CarpoolingApplicant, DTO.CarpoolingApplicantControllerDTO.CarpoolingApplicantDTO>();
            CreateMap<DTO.CarpoolingApplicantControllerDTO.CarpoolingApplicantDTO, CarpoolingApplicant>().ForMember(c => c.CarpoolingNavigation, opt => opt.Ignore())
                                                                                                        .ForMember(c => c.UserNavigation, opt => opt.Ignore());
        }
    }
}