using AutoMapper;
using UoW.Api.Domain.Entities;
using UoW.Api.DTOs.Output;

namespace UoW.Api.Mapping
{
    public class EntityToOutputMappingProfile : Profile
    {
        public EntityToOutputMappingProfile()
        {
            CreateMap<Class, ClassFullDto>();
            CreateMap<Student, StudentDto>();
            
            CreateMap<Student, StudentFullDto>();
            CreateMap<Class, ClassDto>();
        }
    }
}