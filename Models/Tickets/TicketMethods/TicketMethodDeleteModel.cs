using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public void DeleteTicket(Guid ticketGuid, string accesstoken, string connectionString)
        {
            Data.DbHelper.DbWriter writer = new Data.DbHelper.DbWriter();

            List<KeyValuePair<string, string>> parameters = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Guid", ticketGuid.ToString()),
                new KeyValuePair<string, string>("AccessToken", accesstoken)
            };

            int result = writer.WriteUsingProcedure(connectionString, "DeleteTicket", parameters);
        }
    }
}
