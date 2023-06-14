using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.UserOperations.Commands.CreateUser;
using ToDoWebApi.Applications.UserOperations.Commands.DeleteUser;
using ToDoWebApi.Applications.UserOperations.Commands.UpdateUser;
using ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.Applications.UserOperations.UpdateUser;
using ToDoWebApi.DbOperations;
using static ToDoWebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;
using static ToDoWebApi.Applications.UserOperations.Commands.DeleteUser.DeleteUserCommand;
using static ToDoWebApi.Applications.UserOperations.UpdateUser.UpdateUserCommand;

namespace ToDoWebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IToDoDbContext _dbContext;

    readonly IConfiguration _configuration;

    public UserController(IMapper mapper, IToDoDbContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _dbContext = context;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        GetUsersQuery query = new GetUsersQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserDetail(int id)
    {
        GetUserDetailQuery query = new GetUserDetailQuery(_dbContext, _mapper);
        query.UserId = id;
        GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost("newUser")]
    public IActionResult AddUser([FromBody] CreateUserViewModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_dbContext, _mapper);
        command.Model = newUser;
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromBody] UpdateUserModel updateUser, int id)
    {
        UpdateUserCommand command = new UpdateUserCommand(_dbContext, id);
        command.Id = id;
        command.Model = updateUser;
        UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id, string email, string password)
    {
        DeleteUserCommand command = new DeleteUserCommand(_dbContext);
        command.Model = new DeleteUserModel();
        command.Model.Id = id;
        command.Model.Email = email;
        command.Model.Password = password;
        DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}