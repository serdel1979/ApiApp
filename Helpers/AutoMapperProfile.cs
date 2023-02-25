using AppApi.DTO;
using AppApi.Entidades;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AppApi.Helpers
{
    public class AutoMapperProfile : Profile
    {



        public AutoMapperProfile()
        {
            CreateMap<ArchivoDTO, Archivo>()
                .ForMember(dest => dest.Foto, opt => opt.MapFrom(src => src.Foto.OpenReadStream()));

            CreateMap<Archivo, ArchivoRespDTO>();

        }
    }
}
