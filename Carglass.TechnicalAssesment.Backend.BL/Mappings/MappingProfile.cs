using AutoMapper;
using Carglass.TechnicalAssessment.Backend.Dtos;
using Carglass.TechnicalAssessment.Backend.Dtos.Products;
using Carglass.TechnicalAssessment.Backend.Entities.Entities;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Client, ClientDto>().ReverseMap();
        CreateMap<Producto, ProductoDto>().ReverseMap();
    }
}
