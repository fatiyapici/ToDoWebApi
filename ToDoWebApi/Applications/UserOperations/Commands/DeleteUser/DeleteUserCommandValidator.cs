using FluentValidation;

namespace ToDoWebApi.Applications.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.Model.Id).GreaterThan(0);
            RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
        }
    }
}