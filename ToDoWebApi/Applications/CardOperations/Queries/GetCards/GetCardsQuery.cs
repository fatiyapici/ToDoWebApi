using AutoMapper;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.CardOperations.Queries.GetCards
{
    public class GetCardsQuery
    {
        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetCardsQuery(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<CardsViewModel> Handle()
        {
            var cardList = _dbContext.Cards
            .OrderBy(x => x.Id).ToList();

            List<CardsViewModel> cm = _mapper.Map<List<CardsViewModel>>(cardList);
            return cm;
        }
    }

    public class CardsViewModel
    {
        public string Name { get; set; }
        public string Status { get; set; }
    }
}