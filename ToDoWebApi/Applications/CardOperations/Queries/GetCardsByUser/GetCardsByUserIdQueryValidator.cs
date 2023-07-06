using FluentValidation;

namespace ToDoWebApi.Applications.CardOperations.Queries.GetCardsByUser
{
    public class GetCardsByUserQueryValidator : AbstractValidator<GetCardsByUserQuery>
    {
        public GetCardsByUserQueryValidator()
        {
            RuleFor(query => query.UserId).GreaterThan(0);
        }
    }
}