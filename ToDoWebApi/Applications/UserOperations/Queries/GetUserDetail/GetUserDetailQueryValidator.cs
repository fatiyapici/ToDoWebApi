using FluentValidation;

namespace ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
        }
    }
}