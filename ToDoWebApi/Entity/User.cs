using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoWebApi.Entity
{
    public class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<CardUser> UserCards { get; set; }
    }
}