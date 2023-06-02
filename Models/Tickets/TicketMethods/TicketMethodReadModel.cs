using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<Ticket> GetTicketDetail(string accesstoken, Guid ticketGuid, string connectionString)
        {
            Data.DbHelper.GenericRead<Ticket> helper = new Data.DbHelper.GenericRead<Ticket>();
            string query = "SELECT * FROM GetTicketDetail ('" + accesstoken + "', '" + ticketGuid + "')";
            try
            {
                Ticket ticket = new Ticket();
                var result = await helper.Read(query, connectionString, null);
                foreach(var item in result)
                {
                    ticket = item;
                    break;
                }
                return ticket;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
