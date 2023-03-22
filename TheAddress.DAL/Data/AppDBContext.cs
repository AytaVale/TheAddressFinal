using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using TheAddress.DAL.DBModel;
using Property = TheAddress.DAL.DBModel.Property;

namespace TheAddress.DAL.Data
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropertyCategory> PropertyCategories { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<PropertyDocument> PropertyDocuments { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}