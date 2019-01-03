using model;
using DTO;
using System.Linq;
using AutoMapper;

namespace api.Infrastructure {
    public class MappingProfile : AutoMapper.Profile {
        public MappingProfile() {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>().ForMember(u => u.Carpooling, opt => opt.Ignore()).AfterMap((uDTO, u) => AddOrUpdateCarpooling(uDTO, u))
                                    .ForMember(u => u.Car, opt => opt.Ignore()).AfterMap((uDTO, u) => AddOrUpdateCar(uDTO, u));
            CreateMap<User, UserDTORegistration>().ReverseMap();
            CreateMap<Car, CarDTO>().ReverseMap();
            CreateMap<Carpooling, CarpoolingDTO>().ReverseMap();
            CreateMap<CarpoolingApplicant, CarpoolingApplicantDTO>().ReverseMap();
            CreateMap<PrivateMessage, PrivateMessageDTO>().ReverseMap();
        }

        private void AddOrUpdateCarpooling(UserDTO userDTO, User user) {
            for(int i = 0; i < user.Carpooling.Count(); i++) {
                Carpooling carpooling = user.Carpooling.ElementAt(i);
                if (userDTO.Carpooling.SingleOrDefault(c => c.Id == carpooling.Id) == null) user.Carpooling.Remove(carpooling);
            }
            foreach(var carpoolingDTO in userDTO.Carpooling) {
                if (carpoolingDTO.Id == 0) user.Carpooling.Add(AutoMapper.Mapper.Map<Carpooling>(carpoolingDTO));
                else AutoMapper.Mapper.Map(carpoolingDTO, user.Carpooling.SingleOrDefault(c => c.Id == carpoolingDTO.Id));
            }
        }

        private void AddOrUpdateCar(UserDTO userDTO, User user) {
            for(int i = 0; i < user.Car.Count(); i++) {
                Car car = user.Car.ElementAt(i);
                if (userDTO.Car.SingleOrDefault(c => c.Id == car.Id) == null) user.Car.Remove(car);
            }
            foreach(var carDTO in userDTO.Car) {
                if (carDTO.Id == 0) user.Car.Add(Mapper.Map<Car>(carDTO));
                else Mapper.Map(carDTO, user.Car.SingleOrDefault(c => c.Id == carDTO.Id));
            }
        }
    }
}