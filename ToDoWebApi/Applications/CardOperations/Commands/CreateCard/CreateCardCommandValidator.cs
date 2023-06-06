using FluentValidation;

namespace ToDoWebApi.Applications.CardOperations.CreateCard
{
    public class CreateCardCommandValidator : AbstractValidator<CreateCardCommand>
    {
        public CreateCardCommandValidator()
        {
            RuleFor(command => command.Model.Name).NotEmpty();
            RuleFor(command => command.Model.Status).NotEmpty();
        }
    }
}