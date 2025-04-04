using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Models;

namespace DTBitzen.Mapper
{
    public class ModelParaDto : Profile
    {
        public ModelParaDto()
        {
            CreateMap<Usuario, UsuarioDto>();
            CreateMap<Sala, SalaDto>();
            CreateMap<Reserva, ReservaDto>();
        }
    }
}
