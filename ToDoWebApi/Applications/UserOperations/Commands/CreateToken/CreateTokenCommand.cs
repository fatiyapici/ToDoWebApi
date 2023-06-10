using AutoMapper;
using ToDoWebApi.DbOperations;
using ToDoWebApi.TokenOperations.Models;

namespace ToDoWebApi.Applications.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public const string ExceptionMessage = "Email and password is wrong.";

        public CreateTokenViewModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CreateTokenCommand(IToDoDbContext dbContext, IMapper mapper, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x =>
                string.Equals(x.Email, Model.Email) &&
                string.Equals(x.Password, Model.Password));

            if (user is null)
                throw new InvalidOperationException(ExceptionMessage);

            TokenHandler handler = new TokenHandler(_configuration);
            Token token = handler.CreateAccessToken(user);

            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

            _dbContext.SaveChanges();

            return token;
        }

        public class CreateTokenViewModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}