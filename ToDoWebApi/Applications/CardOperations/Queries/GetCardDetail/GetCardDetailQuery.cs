using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.CardOperations.Queries.GetCardDetail
{
    public class GetCardDetailQuery
    {
        public const string ExceptionMessage = "Card does not exists";

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public int CardId { get; set; }

        public GetCardDetailQuery(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public CardDetailViewModel Handle()
        {
            var card = _dbContext.Cards
                .Include(uc => uc.Users).ThenInclude(c => c.User)
                .Where(card => card.Id == CardId)
                .SingleOrDefault();

            if (card is null)
            {
                throw new InvalidOperationException(ExceptionMessage);
            }

            CardDetailViewModel cm = _mapper.Map<CardDetailViewModel>(card);

            return cm;
        }
    }
    public class CardDetailViewModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public virtual List<string> Users { get; set; }
    }
}