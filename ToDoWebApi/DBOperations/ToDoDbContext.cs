using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Entity;

namespace ToDoWebApi.DbOperations
{
    public class ToDoDbContext : DbContext, IToDoDbContext
    {
        public ToDoDbContext(DbContextOptions<ToDoDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserCard> UserCards { get; set; }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserCard>()
                .HasKey(uc => new { uc.CardId, uc.UserId });

            modelBuilder.Entity<UserCard>()
                .HasOne(c => c.Card)
                .WithMany()
                .HasForeignKey(ci => ci.CardId);

            modelBuilder.Entity<UserCard>()
                .HasOne(u => u.User)
                .WithMany(x => x.UserCards)
                .HasForeignKey(x => x.UserId);
        }
    }
}