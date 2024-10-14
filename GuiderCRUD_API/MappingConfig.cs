using System.Runtime;
using AutoMapper;
using GuiderCRUD_API.Models;
using GuiderCRUD_API.Models.DTO;
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
            CreateMap<Category, CategoryUpdateDto>().ReverseMap(); 
            CreateMap<Category, CategorCreateDto>().ReverseMap();

            CreateMap<Tag, TagDto>().ReverseMap();
            CreateMap<TagCreateDto, Tag>().ReverseMap();
            CreateMap<TagUpdateDto, Tag>().ReverseMap();

            CreateMap<Venue, VenueDto>().ReverseMap();
            CreateMap<Venue, VenueCreateDto>().ReverseMap(); 
            CreateMap<Venue, VenueUpdateDto>().ReverseMap();


        }
    }
}
