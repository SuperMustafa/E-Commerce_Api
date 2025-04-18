
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistance.Identity
{
    public class IdentityStoreContext:IdentityDbContext
    {
        public IdentityStoreContext(DbContextOptions<IdentityStoreContext> options) 
            : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Address>().ToTable("Addresses");
        }
    }
}
