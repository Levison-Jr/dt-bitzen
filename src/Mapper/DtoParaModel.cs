using AutoMapper;
using DTBitzen.Dtos;
using DTBitzen.Models;

namespace DTBitzen.Mapper
{
    public class DtoParaModel : Profile
    {
        public DtoParaModel()
        {
            CreateMap<UsuarioDto, Usuario>();
        }
    }
}
