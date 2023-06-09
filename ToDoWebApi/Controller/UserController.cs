using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.UserOperations.Commands.CreateToken;
using ToDoWebApi.Applications.UserOperations.Commands.CreateUser;
using ToDoWebApi.Applications.UserOperations.Commands.DeleteUser;
using ToDoWebApi.Applications.UserOperations.Commands.RefreshToken;
using ToDoWebApi.Applications.UserOperations.Commands.UpdateUser;
using ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.Applications.UserOperations.UpdateUser;
using ToDoWebApi.DbOperations;
using ToDoWebApi.TokenOperations.Models;
using static ToDoWebApi.Applications.UserOperations.Commands.CreateToken.CreateTokenCommand;
using static ToDoWebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;
using static ToDoWebApi.Applications.UserOperations.Commands.DeleteUser.DeleteUserCommand;
using static ToDoWebApi.Applications.UserOperations.UpdateUser.UpdateUserCommand;

namespace ToDoWebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IToDoDbContext _context;

    readonly IConfiguration _configuration;

    public UserController(IMapper mapper, IToDoDbContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
        GetUsersQuery query = new GetUsersQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetUserDetail(int id)
    {
        GetUserDetailQuery query = new GetUserDetailQuery(_context, _mapper);
        query.Id = id;
        GetUserDetailQueryValidator validator = new GetUserDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpPost("newUser")]
    public IActionResult AddUser([FromBody] CreateUserViewModel newUser)
    {
        CreateUserCommand command = new CreateUserCommand(_context, _mapper);
        command.Model = newUser;
        CreateUserCommandValidator validator = new CreateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut()]
    public IActionResult UpdateUser([FromBody] UpdateUserViewModel updateUser)
    {
        UpdateUserCommand command = new UpdateUserCommand(_context);
        command.Model = updateUser;
        UpdateUserCommandValidator validator = new UpdateUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id, string email, string password)
    {
        DeleteUserCommand command = new DeleteUserCommand(_context);
        command.Model = new DeleteUserModel();
        command.Model.Id = id;
        command.Model.Email = email;
        command.Model.Password = password;
        DeleteUserCommandValidator validator = new DeleteUserCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPost("connect/token")]
    public ActionResult<Token> CreateToken([FromBody] CreateTokenViewModel login)
    {
        CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
        command.Model = login;
        CreateTokenCommandValidator validator = new CreateTokenCommandValidator();
        validator.ValidateAndThrow(command);
        var token = command.Handle();
        return token;
    }

    [HttpGet("refreshToken")]
    public ActionResult<Token> RefreshToken([FromQuery] string token)
    {
        RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
        command.RefreshToken = token;
        var resultToken = command.Handle();
        return resultToken;
    }
}