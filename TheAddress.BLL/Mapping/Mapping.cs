using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Dtos;

namespace TheAddress.BLL.Mapping
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<PropertyCategory, PropertyCategoryDto>().ReverseMap();
            CreateMap<Property, PropertyDto>().ReverseMap();
            CreateMap<PropertyDocument, PropertyDocumentDto>().ReverseMap();
            CreateMap<District, DistrictDto>().ReverseMap();
        }
    }
}
