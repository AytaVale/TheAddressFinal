using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.DAL.DBModel
{
    public class Property : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public decimal Area { get; set; }
        public string Description { get; set; }
        public string RoomCount { get; set; }
        public string Floor { get; set; }
        public decimal Price { get; set; }
        public int PropertyCategoryId { get; set; }
        public PropertyCategory PropertyCategory { get; set; }
        public int? DistrictId { get; set; }
        public District? District { get; set; }

        public bool? Buy { get; set; }
        public bool? Rent { get; set; }
        public string ProfileDocPath { get; set; }

        public virtual ICollection<PropertyDocument> PropertyDocuments { get; set; }
    }
}
