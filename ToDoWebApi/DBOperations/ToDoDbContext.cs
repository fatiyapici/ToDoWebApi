using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Entity;

namespace ToDoWebApi.DbOperations
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}