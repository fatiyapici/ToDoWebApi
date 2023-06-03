using Microsoft.EntityFrameworkCore;
using ToDoWebApi.Entity;

namespace ToDoWebApi.DbOperations
{
    public interface IToDoDbContext
    {
        DbSet<Card> Cards { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<CardUser> CardUsers { get; set; }

        int SaveChanges();
    }
}