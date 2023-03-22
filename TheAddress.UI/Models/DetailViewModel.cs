using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TheAddress.DAL.DBModel;
using Property = TheAddress.DAL.DBModel.Property;

namespace TheAddress.UI.Models
{
    public class DetailViewModel
    {
        public Property Property { get; set; }
        public IEnumerable<PropertyDocument> PropertyDocument { get; set; }
    }
}
