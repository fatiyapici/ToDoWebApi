using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.UpdateUser
{
    public class UpdateUserCommand
    {
        public const string ExceptionMessageNotFound = "User does not exist.";
        public const string ExceptionMessageEmail = "User E-Mail is wrong.";
        public const string ExceptionMessagePassword = "User Password is wrong.";

        public UpdateUserViewModel Model { get; set; }

        private readonly IToDoDbContext _context;

        public UpdateUserCommand(IToDoDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(x => x.Id == Model.Id);

            if (user is null)
                throw new InvalidOperationException(ExceptionMessageNotFound);

            if (user.Email != Model.Email)
                throw new InvalidOperationException(ExceptionMessageEmail);

            if (user.Password != Model.Password)
                throw new InvalidOperationException(ExceptionMessagePassword);

            user.Password = Model.NewPassword;

            _context.SaveChanges();
        }

        public class UpdateUserViewModel
        {
            public int Id { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string NewPassword { get; set; }
        }
    }
}