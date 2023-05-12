using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public Collection<Ticket> GetTicketList(Guid userGuid)
        {
            Collection<Ticket> list = new Collection<Ticket>();

            for(int i = 0; i < new Random().Next(2, 10); i++)
            {
                list.Add(new Ticket()
                {
                    Id = i,
                    Guid = Guid.NewGuid(),
                    Title = "Implement GetTicketListMethod (" + i + ")",
                    Description = "This method is still using sample content",
                    Project = new Project()
                    {
                        Name = "Replace this placeholder text (" + i + ")",
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
                });
            }

            return list;
        }
    }
}
