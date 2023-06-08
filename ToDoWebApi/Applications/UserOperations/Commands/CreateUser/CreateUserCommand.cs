using AutoMapper;
using ToDoWebApi.DbOperations;
using ToDoWebApi.Entity;

namespace ToDoWebApi.Applications.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public const string ExceptionMessage = "User already exist.";
        
        public CreateUserViewModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateUserCommand(IToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Email == Model.Email);
            if (user != null)
                throw new InvalidOperationException(ExceptionMessage);

            user = _mapper.Map<User>(Model);

            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
        }

        public class CreateUserViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}