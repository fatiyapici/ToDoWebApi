using FluentValidation;

namespace ToDoWebApi.Applications.CardOperations.Queries.GetCardDetail
{
    public class GetCardDetailQueryValidator : AbstractValidator<GetCardDetailQuery>
    {
        public GetCardDetailQueryValidator()
        {
            RuleFor(query => query.Model.Id).GreaterThan(0);
        }
    }
}