using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        public const string ExceptionMessageFound = "User does not exist.";
        public const string ExceptionMessageEmail = "User E-Mail is wrong.";
        public const string ExceptionMessagePassword = "User Password is wrong.";


        public UpdateUserModel Model { get; set; }
        public int Id { get; set; }

        private readonly IToDoDbContext _dbContext;

        public UpdateUserCommand(IToDoDbContext context, int id)
        {
            _dbContext = context;
            Id = id;
        }
        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == Id);
            if (user is null)
                throw new InvalidOperationException(ExceptionMessageFound);
            if (user.Email != Model.Email)
                throw new InvalidOperationException(ExceptionMessageEmail);
            if (user.Password != Model.Password)
                throw new InvalidOperationException(ExceptionMessagePassword);

            user.Password = Model.NewPassword;

            _dbContext.SaveChanges();
        }
        public class UpdateUserModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }
    }
}