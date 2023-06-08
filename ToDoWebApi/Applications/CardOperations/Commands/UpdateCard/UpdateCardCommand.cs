using ToDoWebApi.DbOperations;
using ToDoWebApi.Entity;

namespace ToDoWebApi.Applications.CardOperations.UpdateCard
{
    public class UpdateCardCommand
    {
        public const string ExceptionMessageFound = "Card does not exist.";
        public const string ExceptionMessageName = "Card Name is wrong.";
        public const string ExceptionMessageStatus = "Card Status is wrong.";


        public UpdateCardViewModel Model { get; set; }
        public int Id { get; set; }

        private readonly IToDoDbContext _context;

        public UpdateCardCommand(IToDoDbContext context, int id)
        {
            _context = context;
            Id = id;
        }

        public void Handle()
        {
            var card = _context.Cards.SingleOrDefault(x => x.Id == Id);
            if (card is null)
                throw new InvalidOperationException(ExceptionMessageFound);

            card.Status = Model.NewStatus;
            card.Name = Model.NewName;

            if (card.Users != null)
                card.Users.Clear();

            if (Model.NewUsers != null && Model.NewUsers.Any())
            {
                foreach (var userName in Model.NewUsers)
                {
                    var user = _context.Users.SingleOrDefault(u => u.Email == userName);
                    if (user != null)
                        card.Users.Add(new CardUser { User = user });
                }
            }
            else
            {
                var firstUser = _context.Users.FirstOrDefault();
                if (firstUser != null)
                    card.Users.Add(new CardUser { User = firstUser });
            }

            _context.SaveChanges();
        }

        public class UpdateCardViewModel
        {
            public string NewName { get; set; }
            public Status NewStatus { get; set; }
            public virtual List<string> NewUsers { get; set; }
        }
    }
}