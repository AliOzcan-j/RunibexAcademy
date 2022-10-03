using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Car;
using Entities.DTOs.CarImage;
using Entities.DTOs.Model;
using Entities.DTOs.Payment;
using Entities.DTOs.Rental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Extensions.MappingProfiles
{
    public class EntityMappingProfile : Profile
    {
        public EntityMappingProfile()
        {
            CreateMap<Car, CarDtoForAdd>().ReverseMap();
            CreateMap<Car, CarDtoForUpdate>().ReverseMap();
            CreateMap<Model, ModelDto>().ReverseMap();
            CreateMap<Rental, RentalDtoForAdd>().ReverseMap();
            CreateMap<Rental, RentalDtoForDelete>().ReverseMap();
            CreateMap<Payment, PaymentDtoForAdd>().ReverseMap();
            CreateMap<CarImage, CarImageDtoForAdd>().ReverseMap();
        }
    }
}
