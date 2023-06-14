using ToDoWebApi.DbOperations;
using ToDoWebApi.Entity;

namespace ToDoWebApi.Applications.CardOperations.UpdateCard
{
    public class UpdateCardCommand
    {
        public const string ExceptionMessageFound = "Card does not exist.";
        // public const string ExceptionMessageName = "Card Name is wrong.";
        // public const string ExceptionMessageStatus = "Card Status is wrong.";

        public UpdateCardViewModel Model { get; set; }
        public int Id { get; set; }

        private readonly IToDoDbContext _dbContext;

        public UpdateCardCommand(IToDoDbContext context, int id)
        {
            _dbContext = context;
            Id = id;
        }

        public void Handle()
        {
           var card = _dbContext.Cards.SingleOrDefault(x => x.Id == Id);
            if (card is null)
                throw new InvalidOperationException(ExceptionMessageFound);

            card.Name = Model.NewName != default ? Model.NewName : card.Name;
            card.Status = Model.NewStatus != default ? Model.NewStatus : card.Status;

            _dbContext.SaveChanges();
        }

        public class UpdateCardViewModel
        {
            public string NewName { get; set; }
            public Status NewStatus { get; set; }
        }
    }
}