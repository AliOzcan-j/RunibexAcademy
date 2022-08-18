using AutoMapper;
using Entities.Concrete;
using Entities.DTOs;
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
            CreateMap<Car, CarDto>().ReverseMap();
            CreateMap<Model, ModelDto>().ReverseMap();
        }
    }
}
