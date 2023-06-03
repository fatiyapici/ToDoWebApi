using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.UserOperations.Commands.CreateUser;
using ToDoWebApi.Applications.UserOperations.Queries.GetUserDetail;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.DbOperations;
using static ToDoWebApi.Applications.UserOperations.Commands.CreateUser.CreateUserCommand;

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
        query.UserId = id;
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

    // [HttpPut("{id}")]
    // public IActionResult UpdateCustomer([FromBody] UpdateCustomerViewModel updateCustomer, int id)
    // {
    //     UpdateCustomerCommand command = new UpdateCustomerCommand(_context, id);
    //     command.Id = id;
    //     command.Model = updateCustomer;
    //     UpdateCustomerCommandValidator validator = new UpdateCustomerCommandValidator();
    //     validator.ValidateAndThrow(command);
    //     command.Handle();
    //     return Ok();
    // }

    // [HttpDelete("{id}")]
    // public IActionResult DeleteCustomer(int id, string email, string password)
    // {
    //     DeleteCustomerCommand command = new DeleteCustomerCommand(_context);
    //     command.Model.Id = id;
    //     command.Model.Email = email;
    //     command.Model.Password = password;
    //     DeleteCustomerCommandValidator validator = new DeleteCustomerCommandValidator();
    //     validator.ValidateAndThrow(command);
    //     command.Handle();
    //     return Ok();
    // }

    // [HttpPost("connect/token")]
    // public ActionResult<Token> CreateToken([FromBody] CreateTokenModel login)
    // {
    //     CreateTokenCommand command = new CreateTokenCommand(_context, _mapper, _configuration);
    //     command.Model = login;
    //     var token = command.Handle();
    //     return token;
    // }

    // [HttpGet("refreshToken")]
    // public ActionResult<Token> RefreshToken([FromQuery] string token)
    // {
    //     RefreshTokenCommand command = new RefreshTokenCommand(_context, _configuration);
    //     command.RefreshToken = token;
    //     var resultToken = command.Handle();
    //     return resultToken;
    // }
}