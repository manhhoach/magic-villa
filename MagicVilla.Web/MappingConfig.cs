using AutoMapper;
using MagicVilla.Web.Models.Villa;
using MagicVilla.Web.Models.VillaNumber;

namespace MagicVilla.Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<VillaDTO, VillaCreateDTO>().ReverseMap();
            CreateMap<VillaDTO, VillaUpdateDTO>().ReverseMap();

            CreateMap<VillaNumberDto, VillaNumberCreateDto>().ReverseMap();
            CreateMap<VillaNumberDto, VillaNumberUpdateDto>().ReverseMap();

            //CreateMap<VillaModel, VillaCreateDTO>().ReverseMap();
            //CreateMap<VillaModel, VillaUpdateDTO>().ReverseMap();

            //CreateMap<VillaNumberModel, VillaNumberDto>().ReverseMap();

        }

    }
}
