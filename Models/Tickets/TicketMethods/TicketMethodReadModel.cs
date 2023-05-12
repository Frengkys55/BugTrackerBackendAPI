using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public Ticket GetTicketDetail(Guid userGuid)
        {
            return new Ticket()
            {
                Id = new Random().Next(0, 100),
                Guid = Guid.NewGuid(),
                Title = "Implement GetTicketListMethod (" + new Random().Next(0, 100) + ")",
                Description = "This method is still using sample content",
                Project = new Project()
                {
                    Name = "Replace this placeholder text (" + new Random().Next(0, 100) + ")",
                },
                Comments = null,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                User = new User()
                {
                    Username = "User",
                },
                Severity = new Severity()
                {
                    Title = "Severe"
                },
                Type = new Type()
                {
                    Name = "Debug"
                }
            };
        }
    }
}
