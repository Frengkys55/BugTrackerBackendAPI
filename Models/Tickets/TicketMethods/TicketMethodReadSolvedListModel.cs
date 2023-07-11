using BugTrackerBackendAPI.Models.Tickets;
using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<ICollection<SolvedTicket>> GetAllSolvedTicketList(string accesstoken, string connectionString)
        {
            Collection<SolvedTicket> list = new Collection<SolvedTicket>();

            Data.DbHelper.GenericRead<SolvedTicket> dbRead = new Data.DbHelper.GenericRead<SolvedTicket>();

            string query = "SELECT * FROM GetSolvedTicketsShort ('" + accesstoken.ToString() + "')";
            try
            {
                return await dbRead.Read(query, connectionString);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
