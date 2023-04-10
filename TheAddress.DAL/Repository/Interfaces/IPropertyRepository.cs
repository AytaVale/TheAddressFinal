using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheAddress.DAL.DBModel;

namespace TheAddress.DAL.Repository.Interfaces
{
    public interface IPropertyRepository : IGenericRepository<Property>
    {
        public Task<List<Property>> GetByCategoryIdAsync(int id);
        public Property UpdateProperty(Property item);
    }
}
