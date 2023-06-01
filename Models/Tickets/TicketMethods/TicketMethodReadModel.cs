using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public Ticket GetTicketDetail(string accesstoken, Guid ticketGuid, string connectionString)
        {
            Data.DbHelper.GenericRead<Ticket> helper = new Data.DbHelper.GenericRead<Ticket>();
            string query = "SELECT * FROM GetTicketDetail ('" + accesstoken + "', '" + ticketGuid + "')";
            try
            {
                var result = helper.Read(query, connectionString);
                return result.ToList()[0];
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
