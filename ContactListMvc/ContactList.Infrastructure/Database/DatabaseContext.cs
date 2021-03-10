using ContactList.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<ContactListEntryEntity> ContactListEntry { get; set; }
    }
}
