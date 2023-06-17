using AutoMapper;
using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.CardOperations.Queries.GetCardsByUser
{
    public class GetCardsByUserQuery
    {
        public const string ExceptionMessage = "Card does not exists";

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public CardsDetailsModel Model { get; set; }

        public GetCardsByUserQuery(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public List<CardsDetailsViewModel> Handle()
        {
            var cardList = _dbContext.Cards.Where(x => x.UserId == Model.UserId).ToList();
            List<CardsDetailsViewModel> cm = _mapper.Map<List<CardsDetailsViewModel>>(cardList);
            return cm;
        }
    }

    public class CardsDetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
    }
    public class CardsDetailsModel
    {
        public int UserId { get; set; }
    }
}