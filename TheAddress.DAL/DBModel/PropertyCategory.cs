using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheAddress.DAL.DBModel
{
    public class PropertyCategory : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Property> Properties { get; set; }

    }
}
