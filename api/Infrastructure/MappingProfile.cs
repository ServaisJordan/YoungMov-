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
                                                            .ForMember(u => u.CarpoolingApplicant, opt => opt.Ignore()).AfterMap((uDTO, u) => AddOrDeleteCarpoolingApplicant(uDTO, u))
                                                            .ForMember(u => u.CreatedAt, opt => opt.Ignore())
                                                            .ForMember(u => u.UpdatedAt, opt => opt.Ignore())
                                                            .ForMember(u => u.TrustedCarpoolingDriverUserNavigation, opt => opt.Ignore()).AfterMap((uDTO, u) => AddOrDeleteTrustedCarpoolingDriverUserNavigation(uDTO, u));
            CreateMap<User, DTO.UserControllerDTO.UserDTORegistration>().ReverseMap();
            CreateMap<Car, DTO.UserControllerDTO.CarDTO>().ReverseMap();
            CreateMap<Carpooling, DTO.UserControllerDTO.CarpoolingDTO>().ReverseMap();
            CreateMap<PrivateMessage, DTO.UserControllerDTO.PrivateMessageDTO>().ReverseMap();
            CreateMap<CarpoolingApplicant, DTO.UserControllerDTO.CarpoolingApplicantDTO>().ReverseMap();
            CreateMap<TrustedCarpoolingDriver, DTO.UserControllerDTO.TrustedCarpoolingDriverDTO>().ReverseMap();

            CreateMap<User, DTO.CarpoolingControllerDTO.UserDTO>().ReverseMap();
            CreateMap<Car, DTO.CarpoolingControllerDTO.CarDTO>().ReverseMap();
            CreateMap<Carpooling, DTO.CarpoolingControllerDTO.CarpoolingDTO>();
            CreateMap<DTO.CarpoolingControllerDTO.CarpoolingDTO, Carpooling>().ForMember(c => c.CarpoolingApplicant, opt => opt.Ignore()).AfterMap((cDTO, c) => UpdateCarpoolingApplicant(cDTO, c))
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
        }

        private void AddOrDeleteCarpoolingApplicant(DTO.UserControllerDTO.UserDTO userDTO, User user) {
            for(int i = 0; i < user.CarpoolingApplicant.Count(); i++) {
                CarpoolingApplicant carpoolingApplicant = user.CarpoolingApplicant.ElementAt(i);
                if (userDTO.Carpooling.SingleOrDefault(c => c.Id == carpoolingApplicant.Id) == null) user.CarpoolingApplicant.Remove(carpoolingApplicant);
            }
            foreach(var carpoolingApplicantDTO in userDTO.CarpoolingApplicant) {
                if (carpoolingApplicantDTO.Id == 0) 
                    user.CarpoolingApplicant.Add(Mapper.Map<CarpoolingApplicant>(carpoolingApplicantDTO));
            }
        }

        private void AddOrDeleteTrustedCarpoolingDriverUserNavigation(DTO.UserControllerDTO.UserDTO userDTO, User user) {
            for (int i = 0; i < user.TrustedCarpoolingDriverUserNavigation.Count(); i++) {
                TrustedCarpoolingDriver trustedCarpoolingDriver = user.TrustedCarpoolingDriverUserNavigation.ElementAt(i);
                if (userDTO.TrustedCarpoolingDriverUserNavigation.SingleOrDefault(t => t.Carpooler == trustedCarpoolingDriver.Carpooler || t.User == trustedCarpoolingDriver.User) == null)
                    user.TrustedCarpoolingDriverUserNavigation.Remove(trustedCarpoolingDriver);
            }
            foreach(DTO.UserControllerDTO.TrustedCarpoolingDriverDTO trustedCarpoolingDriverDTO in userDTO.TrustedCarpoolingDriverUserNavigation) {
                if (user.TrustedCarpoolingDriverUserNavigation.SingleOrDefault(t => t.User == trustedCarpoolingDriverDTO.User || t.Carpooler == trustedCarpoolingDriverDTO.Carpooler) == null)
                    user.TrustedCarpoolingDriverUserNavigation.Add(Mapper.Map<TrustedCarpoolingDriver>(trustedCarpoolingDriverDTO));
            }
        }

        private void UpdateCarpoolingApplicant(DTO.CarpoolingControllerDTO.CarpoolingDTO carpoolingDTO, Carpooling carpooling) {
            
            foreach(var carpoolingApplicantDTO in carpoolingDTO.CarpoolingApplicant) {
                if (carpoolingApplicantDTO.Id != 0)
                    Mapper.Map(carpoolingApplicantDTO, carpooling.CarpoolingApplicant.SingleOrDefault(c => c.Id == carpoolingApplicantDTO.Id));
            }
        }
    }
}