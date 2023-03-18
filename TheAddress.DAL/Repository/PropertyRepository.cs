using TheAddress.DAL.Data;
using TheAddress.DAL.DBModel;
using TheAddress.DAL.Repository.Interfaces;

namespace TheAddress.DAL.Repository
{
    public class PropertyRepository : GenericRepository<Property>, IPropertyRepository
    {

        public PropertyRepository(AppDBContext dbContext) : base(dbContext)
        {

        }

        public async Task<List<Property>> GetByCategoryIdAsync(int id)
        {
            IQueryable<Property> products = _dbContext.Properties.Where(p => p.PropertyCategoryId == id);

            return products.ToList();
        }
    }
}
