using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Guestbook.Models
{
    public class GuestBookContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }

        public GuestBookContext(DbContextOptions<GuestBookContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
