using Microsoft.EntityFrameworkCore;
using ToDoWebApi.DbOperations;
using ToDoWebApi.Entity;

namespace ToDoWebApi.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ToDoDbContext(serviceProvider.GetRequiredService<DbContextOptions<ToDoDbContext>>()))
            {
                if (context.Cards.Any())
                {
                    return;
                }
                #region Users

                context.Users.AddRange(
                    new User
                    {
                        Name = "Fatih",
                        Surname = "Yapici",
                        Email = "fati@mail.com",
                        Password = "123456"
                    },
                    new User
                    {
                        Name = "Baran",
                        Surname = "Taylan",
                        Email = "baran@mail.com",
                        Password = "123456"
                    });

                context.SaveChanges();

                #endregion

                #region Cards

                context.Cards.AddRange(
                   new Card
                   {
                       UserId = 1,
                       Name = "Shopping",
                       Status = Status.ToDo
                   },
                   new Card
                   {
                       UserId = 2,
                       Name = "Meeting",
                       Status = Status.Done
                   });

                context.SaveChanges();

                #endregion
            }
        }
    }
}