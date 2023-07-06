using AutoMapper;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQuery
    {
        public const string ExceptionMessage = "User does not exist.";

        public UserDetailViewModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserDetailQuery(IToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDetailViewModel Handle()
        {
            var user = _dbContext.Users.SingleOrDefault(x => x.Id == Model.Id);
            if (user is null)
                throw new InvalidOperationException(ExceptionMessage);

            UserDetailViewModel um = _mapper.Map<UserDetailViewModel>(user);
            return um;
        }
    }

    public class UserDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}