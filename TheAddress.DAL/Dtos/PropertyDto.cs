using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.DAL.Dtos
{
    public class PropertyDto : BaseDto
    {
        [DisplayName("Əmlakın adı")]
        public string Name { get; set; }
        [DisplayName("Rayon")]
        public int DistrictId { get; set; }

        [DisplayName("Ünvan")]
        public string Address { get; set; }

        [DisplayName("Ümumi sahə")]
        public decimal Area { get; set; }

        [DisplayName("Açıqlama")]
        public string Description { get; set; }

        [DisplayName("Otaq sayı")]
        public string RoomCount { get; set; }

        [DisplayName("Mərtəbə")]
        public string Floor { get; set; }

        [DisplayName("Qiyməti")]
        public decimal Price { get; set; }

        [DisplayName("Əmlakın növü")]
        public int PropertyCategoryId { get; set; }


        [DisplayName("Almaq")]
        public bool Buy { get; set; }
        [DisplayName("Kirayə")]
        public bool Rent { get; set; }
       

        [DisplayName("Şəkil")]
        public string? ProfileDocPath { get; set; }

        public List<PropertyDocumentDto>? PropertyDocuments { get; set; }

        public List<PropertyCategoryDto>? PropertyCategoryDtos { get; set; }
        public List<DistrictDto>? DistrictDtos { get; set; }
    }
}
