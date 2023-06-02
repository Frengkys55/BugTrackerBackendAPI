using System.Collections.ObjectModel;

namespace BugTrackerBackendAPI.Models
{
    public partial class Ticket
    {
        public async Task<ICollection<Models.Tickets.SeverityModel>> GetAllTicketSeverities(string accesstoken, string connectionString)
        {
            Data.DbHelper.GenericRead< Models.Tickets.SeverityModel> dbRead = new Data.DbHelper.GenericRead<Models.Tickets.SeverityModel> ();

            string query = "SELECT * FROM GetAllTicketSeverity ('" + accesstoken.ToString() + "')";
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
