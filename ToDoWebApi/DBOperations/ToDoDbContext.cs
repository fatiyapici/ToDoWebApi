using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Entity;

namespace ToDoWebApi.DbOperations
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<CardUser> CardUsers { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CardUser>()
                .HasKey(x => new { x.CardId, x.UserId });

            modelBuilder.Entity<CardUser>()
                .HasOne(x => x.Card)
                .WithMany(x => x.Users)
                .HasForeignKey(x => x.CardId);

            modelBuilder.Entity<CardUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.UserId);
        }
    }
}