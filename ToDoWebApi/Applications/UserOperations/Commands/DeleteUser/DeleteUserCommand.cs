using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public const string ExceptionMessageNotFound = "User does not exist.";
        public const string ExceptionMessageEmail = "User E-Mail is wrong.";
        public const string ExceptionMessagePassword = "User Password is wrong.";

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
                throw new InvalidOperationException(ExceptionMessageNotFound);

            if (user.Email != Model.Email)
                throw new InvalidOperationException(ExceptionMessageEmail);

            if (user.Password != Model.Password)
                throw new InvalidOperationException(ExceptionMessagePassword);
                
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