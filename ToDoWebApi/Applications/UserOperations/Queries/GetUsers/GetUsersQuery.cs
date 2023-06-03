using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUsersQuery(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var userList = _dbContext.Users
            .OrderBy(x => x.Id).ToList();

            List<UsersViewModel> um = _mapper.Map<List<UsersViewModel>>(userList);
            return um;
        }
    }

    public class UsersViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}