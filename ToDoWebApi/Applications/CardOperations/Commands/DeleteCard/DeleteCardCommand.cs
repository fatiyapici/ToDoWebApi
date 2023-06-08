using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.CardOperations.Commands.DeleteCard
{
    public class DeleteCardCommand
    {
        public const string ExceptionMessageFound = "Card does not exist.";

        public DeleteCardViewModel Model { get; set; }

        private readonly IToDoDbContext _dbContext;

        public DeleteCardCommand(IToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var card = _dbContext.Cards.Find(Model.Id);
            if (card is null)
                throw new InvalidOperationException(ExceptionMessageFound);

            _dbContext.Cards.Remove(card);
            _dbContext.SaveChanges();
        }

        public class DeleteCardViewModel
        {
            public int Id { get; set; }
        }
    }
}