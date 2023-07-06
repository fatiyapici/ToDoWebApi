using FluentValidation;

namespace ToDoWebApi.Applications.CardOperations.Commands.DeleteCard
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator()
        {
            RuleFor(command => command.CardId).GreaterThan(0);
        }
    }
}