using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Applications.CardOperations.Commands.DeleteCard;
using ToDoWebApi.Applications.CardOperations.Commands.UpdateCard;
using ToDoWebApi.Applications.CardOperations.CreateCard;
using ToDoWebApi.Applications.CardOperations.Queries.GetCardDetail;
using ToDoWebApi.Applications.CardOperations.Queries.GetCards;
using ToDoWebApi.Applications.CardOperations.Queries.GetCardsByUser;
using ToDoWebApi.Applications.CardOperations.UpdateCard;
using ToDoWebApi.DbOperations;
using static ToDoWebApi.Applications.CardOperations.Commands.DeleteCard.DeleteCardCommand;
using static ToDoWebApi.Applications.CardOperations.UpdateCard.UpdateCardCommand;
namespace ToDoWebApi.Controllers;

[ApiController]
[Route("[controller]s")]
public class CardController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IToDoDbContext _dbContext;

    readonly IConfiguration _configuration;

    public CardController(IMapper mapper, IToDoDbContext context, IConfiguration configuration)
    {
        _mapper = mapper;
        _dbContext = context;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult GetCards()
    {
        GetCardsQuery query = new GetCardsQuery(_dbContext, _mapper);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("{id}")]
    public IActionResult GetCardDetail(int id)
    {
        GetCardDetailQuery query = new GetCardDetailQuery(_dbContext, _mapper);
        query.Model = new CardDetailViewModel();
        query.Model.Id = id;
        GetCardDetailQueryValidator validator = new GetCardDetailQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }

    [HttpGet("user/{userId}")]
    public IActionResult GetCardsDetailsByUser(int userId)
    {
        GetCardsByUserQuery query = new GetCardsByUserQuery(_dbContext, _mapper);
        query.UserId = userId;
        GetCardsByUserQueryValidator validator = new GetCardsByUserQueryValidator();
        validator.ValidateAndThrow(query);
        var result = query.Handle();
        return Ok(result);
    }


    [HttpPost("newCard")]
    public IActionResult AddCard([FromBody] CreateCardViewModel newCard)
    {
        CreateCardCommand command = new CreateCardCommand(_dbContext, _mapper);
        command.Model = newCard;
        CreateCardCommandValidator validator = new CreateCardCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult UpdateCard([FromBody] UpdateCardViewModel updateCard, int id)
    {
        UpdateCardCommand command = new UpdateCardCommand(_dbContext, id);
        command.Id = id;
        command.Model = updateCard;
        UpdateCardCommandValidator validator = new UpdateCardCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        DeleteCardCommand command = new DeleteCardCommand(_dbContext);
        command.CardId = id;
        DeleteCardCommandValidator validator = new DeleteCardCommandValidator();
        validator.ValidateAndThrow(command);
        command.Handle();
        return Ok();
    }
}