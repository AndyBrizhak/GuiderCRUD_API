using System.Runtime;
using AutoMapper;
using GuiderCRUD_API.Models;
using GuiderCRUD_API.Models.DTO.CategoryDTOs;
using GuiderCRUD_API.Models.DTO.TagDTOs;
using GuiderCRUD_API.Models.DTO.VenueDTOs;

namespace GuiderCRUD_API
{
    public class MappingConfig:Profile
    {
        public MappingConfig() 
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<Tag, TagDto>().ReverseMap(); 

            CreateMap<Venue, VenueDto>().ReverseMap();

        }
    }
}
