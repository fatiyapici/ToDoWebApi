using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQuery
    {
        public const string ExceptionMessage = "User does not exist.";
        public int UserId { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserDetailQuery(IToDoDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public UserDetailViewModel Handle()
        {
            var user = _dbContext.Users
                .Include(uc => uc.UserCards).ThenInclude(c => c.Card)
                .Where(user => user.Id == UserId)
                .SingleOrDefault();

            if (user is null)
            {
                throw new InvalidOperationException(ExceptionMessage);
            }

            UserDetailViewModel um = _mapper.Map<UserDetailViewModel>(user);

            return um;
        }
    }

    public class UserDetailViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual List<string> UserCards { get; set; }
    }
}
