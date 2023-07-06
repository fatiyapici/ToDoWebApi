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

        public CardDetailViewModel Model { get; set; }

        public GetCardDetailQuery(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public CardDetailViewModel Handle()
        {
            var card = _dbContext.Cards.Include(x => x.User).Where(ci => ci.Id == Model.Id)
                .SingleOrDefault(x => x.Id == Model.Id);

            if (card is null)
                throw new InvalidOperationException(ExceptionMessage);

            CardDetailViewModel cm = _mapper.Map<CardDetailViewModel>(card);
            return cm;
        }
    }

    public class CardDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string OwnerEmail { get; set; }
    }
}