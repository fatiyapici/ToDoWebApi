using FluentValidation;
using ToDoWebApi.Applications.CardOperations.UpdateCard;

namespace ToDoWebApi.Applications.CardOperations.Commands.UpdateCard
{
    public class UpdateCardCommandValidator : AbstractValidator<UpdateCardCommand>
    {
        public UpdateCardCommandValidator()
        {
            RuleFor(command => command.Model.NewName).NotEmpty().WithMessage("New name is required.");
            RuleFor(command => command.Model.NewStatus).NotNull().WithMessage("New status is required.");
            RuleFor(command => command.Model.NewStatus).IsInEnum().WithMessage("Invalid status value.");
        }
    }
}