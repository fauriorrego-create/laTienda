using AutoMapper;
using laTienda.DTOs;
using laTienda.Models;

namespace laTienda.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Categoria, CategoriaDTO>();

            CreateMap<Producto, ProductoDTO>()
                .ForMember(
                    dest => dest.CategoriaNombre,
                    opt => opt.MapFrom(src => src.IdcategoriaNavigation.Nombrecategoria)
                );

            CreateMap<ProductoCreateDTO, Producto>();
        }
    }
}