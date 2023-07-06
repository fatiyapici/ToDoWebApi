using AutoMapper;
using ToDoWebApi.DbOperations;
using ToDoWebApi.Entity;

namespace ToDoWebApi.Applications.CardOperations.CreateCard
{
    public class CreateCardCommand
    {
        public const string ExceptionMessage = "Card already exists";

        public CreateCardViewModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateCardCommand(IToDoDbContext context, IMapper mapper)
        {
            _dbContext = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Name.Equals(Model.Name) && x.Status.Equals(Model.Status));
            if (card != null)
                throw new InvalidOperationException(ExceptionMessage);

            card = _mapper.Map<Card>(Model);

            _dbContext.Cards.Add(card);
            _dbContext.SaveChanges();
        }
    }

    public class CreateCardViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public Status Status { get; set; }
    }
}