using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace demoAPI.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options): base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            foreach (var EntityType in builder.Model.GetEntityTypes()) 
            {
                var tableName = EntityType.GetTableName();
                if (tableName.StartsWith("AspNet")) 
                {
                    var newName = tableName.Substring(6);

                    EntityType.SetTableName(newName);
                }
            }

        }
       
    }
}
