using ToDoWebApi.DbOperations;
using ToDoWebApi.TokenOperations.Models;

namespace ToDoWebApi.Applications.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public const string ExceptionMessage = "Valid Refresh Token is not found.";

        public string RefreshToken { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public RefreshTokenCommand(IToDoDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.RefreshToken == RefreshToken && x.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler handler = new TokenHandler(_configuration);
                Token token = handler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _dbContext.SaveChanges();
                
                return token;
            }
            else
                throw new InvalidOperationException(ExceptionMessage);
        }
    }
}