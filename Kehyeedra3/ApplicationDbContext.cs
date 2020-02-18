using Microsoft.EntityFrameworkCore;
using Kehyeedra3.Services.Models;

namespace Kehyeedra3
{
    class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        //public DbSet<Fishing> Fishing { get; set; }
    }
}
