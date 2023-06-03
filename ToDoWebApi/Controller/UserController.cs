using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.UserOperations.Queries.GetUsers;
using ToDoWebApi.DbOperations;

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
    public IActionResult GetCustomers()
    {
        GetUsersQuery query = new GetUsersQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

}