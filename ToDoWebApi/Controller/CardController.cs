using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.CardOperations.CreateCard;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class CardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IToDoDbContext _context;

    readonly IConfiguration _configuration;

    public CardController(IMapper mapper, IToDoDbContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _context = context;
        _configuration = configuration;
    }
    [HttpPost("newCard")]
    public IActionResult AddCard([FromBody] CreateCardViewModel newCard)
    {
        CreateCardCommand command = new CreateCardCommand(_context, _mapper);
        command.Model = newCard;
        CreateCardCommandValidator validator = new CreateCardCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}