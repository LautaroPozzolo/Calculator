using AutoMapper;
using Domain.Models.Dtos;

namespace Domain.Models.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Operation, OperationDTO>();
            CreateMap<OperationDTO, Operation>();
        } 
    }
}
