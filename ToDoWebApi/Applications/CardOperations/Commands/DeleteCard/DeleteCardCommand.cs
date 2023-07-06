using ToDoWebApi.DbOperations;

namespace ToDoWebApi.Applications.CardOperations.Commands.DeleteCard
{
    public class DeleteCardCommand
    {
        public const string ExceptionMessageFound = "Card does not exist.";

        private readonly IToDoDbContext _dbContext;

        public int CardId { get; set; }

        public DeleteCardCommand(IToDoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var card = _dbContext.Cards.Find(CardId);
            if (card is null)
                throw new InvalidOperationException(ExceptionMessageFound);

            _dbContext.Cards.Remove(card);
            _dbContext.SaveChanges();
        }
    }
}