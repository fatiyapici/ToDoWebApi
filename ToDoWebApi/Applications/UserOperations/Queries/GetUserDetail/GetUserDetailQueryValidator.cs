using FluentValidation;

namespace ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}