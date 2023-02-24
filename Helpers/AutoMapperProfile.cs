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
            CreateMap<Archivo, ArchivoDTO>().ReverseMap();

        }
    }
}
