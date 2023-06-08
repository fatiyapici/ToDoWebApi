using FluentValidation;

namespace ToDoWebApi.Applications.CardOperations.Commands.DeleteCard
{
    public class DeleteCardCommandValidator : AbstractValidator<DeleteCardCommand>
    {
        public DeleteCardCommandValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
        }
    }
}