using Api.Dtos;
using AutoMapper;
using Entity;

namespace Api.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Entity.Todo, TodoDto>();

        }
    }
}