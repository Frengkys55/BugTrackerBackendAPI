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
            ignoreProperty.Add("Id");
            ignoreProperty.Add("DateCreated");
            ignoreProperty.Add("DateModified");
            ignoreProperty.Add("Project");
            
            List<KeyValuePair<string, string>> additionParameters = new();
            additionParameters.Add(new KeyValuePair<string, string>("ProjectGuid", projectGuid.ToString()));
            additionParameters.Add(new KeyValuePair<string, string>("AccessToken", accesstoken));
            genericWrite.WriteUsingProcedure(connectionString, "AddTicket", ticket, ignoreProperty, additionParameters);
        }
    }
}
