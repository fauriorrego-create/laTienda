using AutoMapper;
using laTienda.DTOs;
using laTienda.Models;

namespace laTienda.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CATEGORIA
            CreateMap<Categoria, CategoriaDTO>();

            // PRODUCTO -> DTO (SEGURA)
            CreateMap<Producto, ProductoDTO>()
                .ForMember(
                    dest => dest.CategoriaNombre,
                    opt => opt.MapFrom(src =>
                        src.IdcategoriaNavigation != null
                            ? src.IdcategoriaNavigation.Nombrecategoria
                            : string.Empty
                    )
                );

            // DTO -> PRODUCTO
            CreateMap<ProductoCreateDTO, Producto>();
        }
    }
}