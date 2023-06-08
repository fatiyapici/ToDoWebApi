using FluentValidation;
using ToDoWebApi.Applications.UserOperations.UpdateUser;

namespace ToDoWebApi.Applications.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(command => command.Model.Email).NotEmpty().EmailAddress();
            RuleFor(command => command.Model.Password).NotEmpty().MinimumLength(6);
            RuleFor(command => command.Model.NewPassword).NotEmpty().MinimumLength(6);
        }
    }
}