using TheAddress.DAL.DBModel;

namespace TheAddress.UI.Models
{
    public class AllViewModel
    {
        public IEnumerable<PropertyCategory> Categories { get; set; }
        public IEnumerable<Property> Property { get; set; }
        public IEnumerable<District> Districts { get; set; }
        public FilterFormModel FilterForm { get; set; }
    }
}
