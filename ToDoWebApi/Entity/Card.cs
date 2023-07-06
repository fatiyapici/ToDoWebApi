using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApi.Entity
{
    public class Card
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        ToDo = 1,
        Doing = 2,
        Done = 3
    }
}