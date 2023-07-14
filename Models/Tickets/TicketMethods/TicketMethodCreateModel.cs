using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public void CreateTicket(Ticket ticket, Guid projectGuid, string connectionString, string accesstoken)
        {
            // Replace Guid (just to make sure that there is no conflict)
            ticket.Guid = Guid.NewGuid();

            Data.DbHelper.GenericWrite<Ticket> genericWrite = new Data.DbHelper.GenericWrite<Ticket>();
            List<string> ignoreProperty = new List<string>();
            ignoreProperty.Add(nameof(ticket.Id));
            ignoreProperty.Add(nameof(ticket.DateCreated));
            ignoreProperty.Add(nameof(ticket.DateModified));
            ignoreProperty.Add(nameof(ticket.Project));
            ignoreProperty.Add(nameof(ticket.DateSolved));
            
            List<KeyValuePair<string, string>> additionParameters = new();
            additionParameters.Add(new KeyValuePair<string, string>("ProjectGuid", projectGuid.ToString()));
            additionParameters.Add(new KeyValuePair<string, string>("AccessToken", accesstoken));
            genericWrite.WriteUsingProcedure(connectionString, "AddTicket", ticket, ignoreProperty, additionParameters);
        }
    }
}
