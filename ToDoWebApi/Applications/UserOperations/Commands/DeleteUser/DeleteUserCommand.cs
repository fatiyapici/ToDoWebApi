using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public const string ExceptionMessageFound = "User does not exist.";
        public const string ExceptionMessageEmailPassword = "User E-Mail is or Password wrong.";

        public DeleteUserModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;

        public DeleteUserCommand(IToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var user = _dbContext.Users.Find(Model.Id);
            if (user is null)
                throw new InvalidOperationException(ExceptionMessageFound);

            if (!user.Email.Equals(Model.Email)|| !user.Password.Equals(Model.Password))
                throw new InvalidOperationException(ExceptionMessageEmailPassword);

            _dbContext.Users.Remove(user);
            _dbContext.SaveChanges();
        }

        public class DeleteUserModel
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}