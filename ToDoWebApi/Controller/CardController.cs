using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.CardOperations.Commands.DeleteCard;
using ToDoWebApi.Applications.CardOperations.CreateCard;
using ToDoWebApi.Applications.CardOperations.Queries.GetCardDetail;
using ToDoWebApi.Applications.CardOperations.Queries.GetCards;
using ToDoWebApi.DbOperations;
using static ToDoWebApi.Applications.CardOperations.Commands.DeleteCard.DeleteCardCommand;

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

    [HttpGet]
    public IActionResult GetCards()
    {
        GetCardsQuery query = new GetCardsQuery(_context, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetCardDetail(int id)
    {
        GetCardDetailQuery query = new GetCardDetailQuery(_context, _mapper);
        query.CardId = id;
        GetCardDetailQueryValidator validator = new GetCardDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
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

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        DeleteCardCommand command = new DeleteCardCommand(_context);
        command.Model = new DeleteCardViewModel();
        command.Model.Id = id;
        DeleteCardCommandValidator validator = new DeleteCardCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

}